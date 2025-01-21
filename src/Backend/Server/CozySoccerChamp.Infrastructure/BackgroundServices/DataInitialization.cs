using System.Diagnostics;
using AutoMapper;
using CozySoccerChamp.External.SoccerApi.Abstractions;
using CozySoccerChamp.External.SoccerApi.Models.Responses;

namespace CozySoccerChamp.Infrastructure.BackgroundServices;

public class DataInitialization(
    IServiceScopeFactory serviceScopeFactory,
    IMapper mapper,
    ILogger<DataInitialization> logger) : BackgroundService
{
    private ISoccerApiClient _apiClient = null!;
    private ITeamRepository _teamRepository = null!;
    private IMatchRepository _matchRepository = null!;
    private ICompetitionRepository _competitionRepository = null!;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            using var scope = serviceScopeFactory.CreateScope();

            _apiClient = scope.ServiceProvider.GetRequiredService<ISoccerApiClient>();
            _teamRepository = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
            _matchRepository = scope.ServiceProvider.GetRequiredService<IMatchRepository>();
            _competitionRepository = scope.ServiceProvider.GetRequiredService<ICompetitionRepository>();

            logger.LogInformation("---> Job is started...");

            if (await IsInitialized())
            {
                stopwatch.Stop();

                logger.LogInformation("<--- Data is initialized. Execution time: {ExecutionTime}",
                    stopwatch.Elapsed.ToString("mm':'ss':'fff"));

                return;
            }

            var season = 2024; //DateTime.UtcNow.Year;

            await CompetitionInitializationAsync(season);
            await TeamInitializationAsync(season);
            await MatchInitializationAsync(season);

            stopwatch.Stop();

            logger.LogInformation("<--- Job is ended. Execution time: {ExecutionTime}",
                stopwatch.Elapsed.ToString("mm':'ss':'fff"));
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            logger.LogError(ex, "<--- Job is ended with exception! Execution time: {ExecutionTime}",
                stopwatch.Elapsed.ToString("mm':'ss':'fff"));
        }
    }

    private async Task<bool> IsInitialized()
    {
        return await _teamRepository.AnyAsync() && await _matchRepository.AnyAsync();
    }
    
    private async Task CompetitionInitializationAsync(int season)
    {
        if (await _competitionRepository.AnyAsync())
            return;

        var (competitionsData, resultSetData) = await _apiClient.GetCompetitionAsync(season);

        if (competitionsData is null && resultSetData is null)
            return;

        var competition = new Competition
        {
            Name = competitionsData!.Name,
            EmblemUrl = competitionsData.EmblemUrl,
            Started = resultSetData.First.ToUniversalTime(),
            Finished = resultSetData.Last.ToUniversalTime()
        };

        await _competitionRepository.AddAsync(competition);
    }

    private async Task TeamInitializationAsync(int season)
    {
        if (await _teamRepository.AnyAsync())
            return;

        var teamsData = await _apiClient.GetTeamsAsync(season);

        if (teamsData is null)
            return;

        var teams = mapper.Map<ICollection<Team>>(teamsData);

        await _teamRepository.AddRangeAsync(teams);
    }

    private async Task MatchInitializationAsync(int season)
    {
        if (await _matchRepository.AnyAsync())
            return;

        var matchesData = await _apiClient.GetMatchesAsync(season);

        if (matchesData is null)
            return;

        var competitionId = await GetCompetitionId(matchesData.FirstOrDefault()!.Competition);

        var matches = new List<Match>();

        foreach (var match in matchesData)
        {
            var teamHome = await _teamRepository.FindAsync(x => x.ExternalTeamId == match.HomeTeam.Id);
            var teamAway = await _teamRepository.FindAsync(x => x.ExternalTeamId == match.AwayTeam.Id);

            var matchEntity = mapper.Map<Match>(match);
            matchEntity.TeamHomeId = teamHome?.Id;
            matchEntity.TeamAwayId = teamAway?.Id;
            matchEntity.CompetitionId = competitionId;
            matchEntity.MatchResult = new MatchResult();

            matches.Add(matchEntity);
        }

        await _matchRepository.AddRangeAsync(matches);

        #region local methods

        async Task<int?> GetCompetitionId(CompetitionResponse source)
        {
            var competition = await _competitionRepository.FindAsync(x => x.Name == source.Name);

            return competition?.Id;
        }

        #endregion
    }
}