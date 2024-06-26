using CozySoccerChamp.External.SoccerApi.Models.Responses;

namespace CozySoccerChamp.External.SoccerApi.Abstractions;

public interface ISoccerApiClient
{
    Task<IReadOnlyCollection<MatchResponse>> GetMatchesAsync(int season);
    Task<IReadOnlyCollection<TeamResponse>> GetTeamsAsync(int season);
}