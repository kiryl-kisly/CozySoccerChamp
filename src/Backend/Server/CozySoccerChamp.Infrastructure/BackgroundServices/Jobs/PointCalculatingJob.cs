using System.Diagnostics;
using CozySoccerChamp.Infrastructure.BackgroundServices.Jobs.Settings;
using Quartz;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

[DisallowConcurrentExecution]
public class PointCalculatingJob(
    IMatchResultRepository matchResultRepository,
    IApplicationUserRepository userRepository,
    PointCalculateSettings settings,
    ILogger<PointCalculatingJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            logger.Log(LogLevel.Information, $"---> Job is started...");

            await ExecuteAsync();

            stopwatch.Stop();
            logger.Log(LogLevel.Information, $"<--- Job is ended. Execution time: {stopwatch.Elapsed:mm':'ss':'fff}");
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logger.Log(LogLevel.Error, $"<--- Job is ended. Execution time: {stopwatch.Elapsed:mm':'ss':'fff}.\nException: {ex}");
        }
    }

    private async Task ExecuteAsync()
    {
        var finishedMatches = await GetFinishedMatchesAsync();

        if (finishedMatches.Count == 0)
            return;

        var users = await GetUsersWithPredictionsAsync(finishedMatches);

        if (users.Count == 0)
            return;

        var usersToUpdate = ProcessPredictions(users, finishedMatches);

        if (usersToUpdate.Count != 0)
            await userRepository.UpdateRangeAsync(usersToUpdate);
    }

    private async Task<List<MatchResult>> GetFinishedMatchesAsync()
    {
        return await matchResultRepository.GetAllAsQueryable(asNoTracking: true, x => x.Match)
            .Where(x => x.Status == MatchResultStatus.Finished)
            .ToListAsync();
    }

    private async Task<List<ApplicationUser>> GetUsersWithPredictionsAsync(List<MatchResult> finishedMatches)
    {
        return await userRepository.GetAllAsQueryable(includes: x => x.Predictions)
            .Where(x => x.Predictions.Any(p => finishedMatches.Select(m => m.MatchId).Contains(p.MatchId)))
            .ToListAsync();
    }

    private List<ApplicationUser> ProcessPredictions(List<ApplicationUser> users, List<MatchResult> finishedMatches)
    {
        var usersToUpdate = new List<ApplicationUser>();

        foreach (var user in users)
        {
            foreach (var prediction in user.Predictions)
            {
                var matchResult = finishedMatches.FirstOrDefault(x => x.MatchId == prediction.MatchId);

                if (matchResult is null)
                    continue;

                prediction.PointPerMatch = GetUserPoint(matchResult, prediction);
                prediction.Coefficient = GetCoefficient(matchResult);

                if (!usersToUpdate.Contains(user))
                    usersToUpdate.Add(user);
            }
        }

        return usersToUpdate;
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