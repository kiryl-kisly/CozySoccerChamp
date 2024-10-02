using System.Diagnostics;
using CozySoccerChamp.External.SoccerApi.Abstractions;
using CozySoccerChamp.External.SoccerApi.Models.Responses;
using Quartz;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

[DisallowConcurrentExecution]
public sealed class MatchDataProcessingJob(
    ISoccerApiClient soccerApiClient,
    ITeamRepository teamRepository,
    IMatchRepository matchRepository,
    ILogger<MatchDataProcessingJob> logger) : IJob
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
        var season = DateTime.UtcNow.Year;

        await MatchesProcessingAsync(season);
    }

    private async Task MatchesProcessingAsync(int season)
    {
        var matchesData = await soccerApiClient.GetMatchesAsync(season);

        if (matchesData is null)
            return;

        await UpdateMatchDataAsync(matchesData);
        await UpdateStartedMatchAsync(matchesData);
        await UpdateFinishedMatchAsync(matchesData);
    }

    private async Task UpdateMatchDataAsync(IReadOnlyCollection<MatchResponse> matchesData)
    {
        var matchesNeedUpdated = await matchRepository.GetAllAsQueryable()
            .Where(x => x.TeamHomeId == null || x.TeamAwayId == null)
            .ToListAsync();

        foreach (var match in matchesNeedUpdated)
        {
            var updateData = matchesData.FirstOrDefault(x => x.Id == match.ExternalMatchId);

            if (updateData?.HomeTeam.Id == null && updateData?.AwayTeam.Id == null)
                continue;

            if (updateData.HomeTeam.Id is not null)
            {
                var teamHome = await teamRepository.FindAsync(x => x.ExternalTeamId == updateData.HomeTeam.Id);
                match.TeamHomeId = teamHome!.Id;
            }

            if (updateData.AwayTeam.Id is not null)
            {
                var teamAway = await teamRepository.FindAsync(x => x.ExternalTeamId == updateData.AwayTeam.Id);
                match.TeamAwayId = teamAway!.Id;
            }

            await matchRepository.UpdateAsync(match);
        }
    }

    private async Task UpdateStartedMatchAsync(IReadOnlyCollection<MatchResponse> matchesData)
    {
        const string status = "IN_PLAY";

        var startedMatches = matchesData
            .Where(x => string.Equals(status, x.Status, StringComparison.CurrentCultureIgnoreCase));

        foreach (var startedMatch in startedMatches)
        {
            var match = await matchRepository.FindAsync(x => x.ExternalMatchId == startedMatch.Id, includes: x => x.MatchResult);

            if (match is null)
                continue;

            match.MatchResult.Status = MatchResultStatus.Started;

            await matchRepository.UpdateAsync(match);
        }
    }

    private async Task UpdateFinishedMatchAsync(IReadOnlyCollection<MatchResponse> matchesData)
    {
        const string status = "FINISHED";

        var finishedMatches = matchesData
            .Where(x => string.Equals(status, x.Status, StringComparison.CurrentCultureIgnoreCase))
            .Where(x => !string.IsNullOrEmpty(x.MatchResult.Winner));

        foreach (var finishedMatch in finishedMatches)
        {
            var match = await matchRepository
                .FindAsync(x => x.ExternalMatchId == finishedMatch.Id, includes: x => x.MatchResult);

            if (match is null)
                continue;

            var matchResult = match.MatchResult;

            matchResult.Duration = GetDuration(finishedMatch.MatchResult.Duration);
            matchResult.FullTime = GetScore(finishedMatch.MatchResult.FullTime);
            matchResult.HalfTime = GetScore(finishedMatch.MatchResult.HalfTime);
            matchResult.RegularTime = GetScore(finishedMatch.MatchResult.RegularTime);
            matchResult.ExtraTime = GetScore(finishedMatch.MatchResult.ExtraTime);
            matchResult.Penalties = GetScore(finishedMatch.MatchResult.Penalties);
            matchResult.Status = MatchResultStatus.Finished;

            await matchRepository.UpdateAsync(match);

            #region local methods

            DurationStatus GetDuration(string src)
            {
                return src switch
                {
                    "REGULAR" => DurationStatus.Regular,
                    "EXTRA_TIME" => DurationStatus.ExtraTime,
                    "PENALTY_SHOOTOUT" => DurationStatus.PenaltyShootout,
                    _ => default
                };
            }

            string? GetScore(ScoreResponse? src)
            {
                return src is null
                    ? null
                    : $"{src.HomeTeamScore}:{src.AwayTeamScore}";
            }

            #endregion
        }
    }
}