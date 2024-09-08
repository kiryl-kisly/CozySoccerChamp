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
        var finishedMatches = await matchResultRepository.GetAllAsQueryable(asNoTracking: true, x => x.Match)
            .Where(x => x.Status == MatchResultStatus.Finished)
            .ToListAsync();

        if (finishedMatches.Count == 0)
            return;

        foreach (var matchResult in finishedMatches)
        {
            await UpdatePointsForUserAsync(matchResult);
        }
    }

    private async Task UpdatePointsForUserAsync(MatchResult matchResult)
    {
        var users = await userRepository.GetAllAsync(includes: x => x.Predictions);

        foreach (var user in users)
        {
            var prediction = user.Predictions.FirstOrDefault(x => x.MatchId == matchResult.MatchId);

            if (prediction is null)
                continue;

            prediction.PointPerMatch = GetUserPoint();
            prediction.Coefficient = GetCoefficient();

            await userRepository.UpdateAsync(user);

            #region local methods

            int GetUserPoint()
            {
                var (homeTeamScore, awayTeamScore) = GetScore();

                var isPredictedWinner = (homeTeamScore > awayTeamScore && prediction.PredictedHomeScore > prediction.PredictedAwayScore)
                                        || (homeTeamScore < awayTeamScore && prediction.PredictedHomeScore < prediction.PredictedAwayScore)
                                        || (homeTeamScore == awayTeamScore && prediction.PredictedHomeScore == prediction.PredictedAwayScore);

                if (!isPredictedWinner)
                    return 0;

                var userPoint = settings.PointSettings.Outcome; // Балл за правильный исход

                if (homeTeamScore == prediction.PredictedHomeScore || awayTeamScore == prediction.PredictedAwayScore)
                {
                    userPoint = settings.PointSettings.GoalByOneTeam; // Балл исход + за правильное число голов одной из команд
                }

                if (homeTeamScore - awayTeamScore == prediction.PredictedHomeScore - prediction.PredictedAwayScore)
                {
                    userPoint = settings.PointSettings.GoalDifference; // Балл исход + за правильную разницу забитых мячей
                }

                if (homeTeamScore == prediction.PredictedHomeScore && awayTeamScore == prediction.PredictedAwayScore)
                {
                    userPoint = settings.PointSettings.ExactScore; // Балл за точный счет
                }

                return userPoint;
            }

            double GetCoefficient()
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

            (int HomeTeamScore, int AwayTeamScore) GetScore()
            {
                if (matchResult.Duration is DurationStatus.ExtraTime or DurationStatus.PenaltyShootout)
                {
                    var regularTime = matchResult.RegularTime.Split(":");

                    return (Convert.ToInt32(regularTime[0]), Convert.ToInt32(regularTime[1]));
                }

                var fullTime = matchResult.FullTime.Split(":");

                return (Convert.ToInt32(fullTime[0]), Convert.ToInt32(fullTime[1]));
            }

            #endregion
        }
    }
}