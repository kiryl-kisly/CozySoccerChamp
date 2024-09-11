using CozySoccerChamp.External.SoccerApi.Models.Responses;

namespace CozySoccerChamp.External.SoccerApi.Abstractions;

public interface ISoccerApiClient
{
    Task<(CompetitionResponse, ResultSetResponse)> GetCompetitionAsync(int season);
    Task<IReadOnlyCollection<MatchResponse>> GetMatchesAsync(int season);
    Task<IReadOnlyCollection<TeamResponse>> GetTeamsAsync(int season);
}