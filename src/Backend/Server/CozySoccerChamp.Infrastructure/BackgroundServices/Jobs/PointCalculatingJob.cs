using System.Collections.Concurrent;
using CozySoccerChamp.Domain.Entities.User;
using CozySoccerChamp.Infrastructure.BackgroundServices.Jobs.Settings;
using Quartz;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

[DisallowConcurrentExecution]
public sealed class PointCalculatingJob(
    IMatchResultRepository matchResultRepository,
    IApplicationUserRepository userRepository,
    PointCalculateSettings settings,
    ILogger<PointCalculatingJob> logger) : BaseJob(logger)
{
    protected override async Task ExecuteAsync(IJobExecutionContext context)
    {
        var finishedMatches = await GetFinishedMatchesDictAsync();

        if (finishedMatches.Count == 0)
            return;

        var users = await GetUsersWithPredictionsAsync(finishedMatches);

        if (users.Count == 0)
            return;

        var usersToUpdate = ProcessPredictions(users, finishedMatches);

        if (usersToUpdate.Count != 0)
            await userRepository.UpdateRangeAsync(usersToUpdate);
    }

    private async Task<Dictionary<int, MatchResult>> GetFinishedMatchesDictAsync()
    {
        return await matchResultRepository.GetAllAsQueryable(asNoTracking: true, x => x.Match)
            .Where(x => x.Status == MatchResultStatus.Finished)
            .ToDictionaryAsync(x => x.Match.Id);
    }

    private async Task<List<ApplicationUser>> GetUsersWithPredictionsAsync(Dictionary<int, MatchResult> finishedMatchesDict)
    {
        return await userRepository.GetAllAsQueryable(includes: x => x.Predictions)
            .Where(x => x.Predictions.Any(p => finishedMatchesDict.Select(m => m.Key).Contains(p.MatchId)))
            .ToListAsync();
    }

    private List<ApplicationUser> ProcessPredictions(List<ApplicationUser> users, Dictionary<int, MatchResult> finishedMatchesDict)
    {
        var usersToUpdate = new ConcurrentDictionary<long, ApplicationUser>();

        Parallel.ForEach(users, user =>
        {
            var predictionsToUpdate = new ConcurrentBag<Prediction>();

            Parallel.ForEach(user.Predictions, prediction =>
            {
                if (finishedMatchesDict.TryGetValue(prediction.MatchId, out var matchResult))
                {
                    prediction.PointPerMatch = GetUserPoint(matchResult, prediction);
                    prediction.Coefficient = GetCoefficient(matchResult);
                }

                predictionsToUpdate.Add(prediction);
            });

            if (predictionsToUpdate.IsEmpty)
                return;

            user.Predictions = predictionsToUpdate.ToList();

            usersToUpdate.TryAdd(user.TelegramUserId, user);
        });

        return usersToUpdate.Values.ToList();
    }

    private int GetUserPoint(MatchResult matchResult, Prediction prediction)
    {
        var (homeTeamScore, awayTeamScore) = GetScore(matchResult);

        var isPredictedWinner = (homeTeamScore > awayTeamScore && prediction.PredictedHomeScore > prediction.PredictedAwayScore)
                                || (homeTeamScore < awayTeamScore && prediction.PredictedHomeScore < prediction.PredictedAwayScore)
                                || (homeTeamScore == awayTeamScore && prediction.PredictedHomeScore == prediction.PredictedAwayScore);

        if (!isPredictedWinner)
            return 0;

        var userPoint = settings.PointSettings.Outcome; // Балл за правильный исход

        if (homeTeamScore - awayTeamScore == prediction.PredictedHomeScore - prediction.PredictedAwayScore)
        {
            userPoint = settings.PointSettings.GoalDifference; // Балл за правильную разницу забитых мячей
        }

        if (homeTeamScore == prediction.PredictedHomeScore && awayTeamScore == prediction.PredictedAwayScore)
        {
            userPoint = settings.PointSettings.ExactScore; // Балл за точный счет
        }

        return userPoint;
    }

    private double GetCoefficient(MatchResult matchResult)
    {
        return matchResult.Match.Stage switch
        {
            "LEAGUE_STAGE" => settings.CoefficientSettings.LeagueStage,
            "LAST_16" => settings.CoefficientSettings.Last16,
            "QUARTER_FINALS" => settings.CoefficientSettings.QuarterFinals,
            "SEMI_FINALS" => settings.CoefficientSettings.SemiFinals,
            "FINAL" => settings.CoefficientSettings.Final,
            _ => settings.CoefficientSettings.LeagueStage
        };
    }

    private static (int HomeTeamScore, int AwayTeamScore) GetScore(MatchResult matchResult)
    {
        int homeTeamScore;
        int awayTeamScore;

        if (matchResult.Duration is DurationStatus.ExtraTime or DurationStatus.PenaltyShootout)
        {
            var regularTime = matchResult.RegularTime.Split(":");
            homeTeamScore = int.Parse(regularTime[0]);
            awayTeamScore = int.Parse(regularTime[1]);

            return (homeTeamScore, awayTeamScore);
        }

        var fullTime = matchResult.FullTime.Split(":");
        homeTeamScore = int.Parse(fullTime[0]);
        awayTeamScore = int.Parse(fullTime[1]);

        return (homeTeamScore, awayTeamScore);
    }
}