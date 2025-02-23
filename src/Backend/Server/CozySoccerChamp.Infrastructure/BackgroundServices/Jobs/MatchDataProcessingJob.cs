using CozySoccerChamp.External.SoccerApi.Abstractions;
using CozySoccerChamp.External.SoccerApi.Models.Responses;
using Quartz;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

[DisallowConcurrentExecution]
public sealed class MatchDataProcessingJob(
    ISoccerApiClient soccerApiClient,
    ITeamRepository teamRepository,
    IMatchRepository matchRepository,
    ILogger<MatchDataProcessingJob> logger) : BaseJob(logger)
{
    protected override async Task ExecuteAsync(IJobExecutionContext context)
    {
        // TODO: Придумать как обрабатывать это. Например брать дату из Competitions
        var season = 2024;

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
        var matchesNeedUpdated = await matchRepository.GetAllAsQueryable(includes: x => x.MatchResult)
            .Where(x => x.MatchResult.Status == MatchResultStatus.Scheduled || x.TeamHomeId == null || x.TeamAwayId == null)
            .ToListAsync();

        if (matchesNeedUpdated.Count == 0)
            return;

        var matchesToUpdate = new List<Match>();

        foreach (var match in matchesNeedUpdated)
        {
            var updateData = matchesData.FirstOrDefault(x => x.Id == match.ExternalMatchId);
            if (updateData is null)
                continue;

            if (IsChangeStatus(match.MatchResult, updateData.Status))
            {
                match.MatchResult.Status = MatchResultStatus.Timed;
                match.MatchTime = updateData.StartDateUtc;
            }

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

            matchesToUpdate.Add(match);
        }

        if (matchesToUpdate.Count != 0)
            await matchRepository.UpdateRangeAsync(matchesToUpdate);

        #region Local Methods

        bool IsChangeStatus(MatchResult matchResult, string status)
        {
            return matchResult.Status == MatchResultStatus.Scheduled && status == "TIMED";
        }

        #endregion
    }

    private async Task UpdateStartedMatchAsync(IReadOnlyCollection<MatchResponse> matchesData)
    {
        const string status = "IN_PLAY";

        var startedMatchIds = matchesData
            .Where(x => string.Equals(status, x.Status, StringComparison.CurrentCultureIgnoreCase))
            .Select(x => x.Id)
            .ToList();

        if (startedMatchIds.Count == 0)
            return;

        var matchesToUpdate = await matchRepository.GetAllAsQueryable()
            .Where(x => startedMatchIds.Contains(x.ExternalMatchId))
            .Include(x => x.MatchResult)
            .ToListAsync();

        foreach (var match in matchesToUpdate)
        {
            if (match.MatchResult is not null)
            {
                match.MatchResult.Status = MatchResultStatus.Started;
            }
        }

        if (matchesToUpdate.Count != 0)
            await matchRepository.UpdateRangeAsync(matchesToUpdate);
    }

    private async Task UpdateFinishedMatchAsync(IReadOnlyCollection<MatchResponse> matchesData)
    {
        const string status = "FINISHED";

        var finishedMatchIds = matchesData
            .Where(x => string.Equals(status, x.Status, StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(x.MatchResult.Winner))
            .Select(x => x.Id)
            .ToList();

        if (finishedMatchIds.Count == 0)
            return;

        var matchesToUpdate = await matchRepository.GetAllAsQueryable()
            .Where(x => finishedMatchIds.Contains(x.ExternalMatchId))
            .Include(x => x.MatchResult)
            .ToListAsync();

        foreach (var match in matchesToUpdate)
        {
            var finishedMatchData = matchesData.FirstOrDefault(x => x.Id == match.ExternalMatchId);

            if (finishedMatchData is null || match.MatchResult is null)
                continue;

            var matchResult = match.MatchResult;

            matchResult.Duration = GetDuration(finishedMatchData.MatchResult.Duration);
            matchResult.FullTime = GetScore(finishedMatchData.MatchResult.FullTime);
            matchResult.HalfTime = GetScore(finishedMatchData.MatchResult.HalfTime);
            matchResult.RegularTime = GetScore(finishedMatchData.MatchResult.RegularTime);
            matchResult.ExtraTime = GetScore(finishedMatchData.MatchResult.ExtraTime);
            matchResult.Penalties = GetScore(finishedMatchData.MatchResult.Penalties);
            matchResult.Status = MatchResultStatus.Finished;
        }

        if (matchesToUpdate.Count != 0)
            await matchRepository.UpdateRangeAsync(matchesToUpdate);

        #region Local Methods

        static DurationStatus GetDuration(string src)
        {
            return src switch
            {
                "REGULAR" => DurationStatus.Regular,
                "EXTRA_TIME" => DurationStatus.ExtraTime,
                "PENALTY_SHOOTOUT" => DurationStatus.PenaltyShootout,
                _ => default
            };
        }

        static string? GetScore(ScoreResponse? src)
        {
            return src is null
                ? null
                : $"{src.HomeTeamScore}:{src.AwayTeamScore}";
        }

        #endregion
    }
}