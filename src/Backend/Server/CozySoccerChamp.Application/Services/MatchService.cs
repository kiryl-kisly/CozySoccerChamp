using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Services;

public class MatchService(IMatchRepository matchRepository, IMapper mapper) : IMatchService
{
    public async Task<IReadOnlyCollection<MatchResponse>> GetAllAsync()
    {
        var matches = await matchRepository.GetAllAsync(asNoTracking: true,
            x => x.TeamHome,
            x => x.TeamAway,
            x => x.MatchResult);

        return mapper.Map<IReadOnlyCollection<MatchResponse>>(matches);
    }

    public async Task<MatchResponse> GetByIdAsync(int matchId)
    {
        var match = await matchRepository.GetByIdAsync(matchId,
            x => x.TeamHome,
            x => x.TeamAway,
            x => x.MatchResult);

        if (match is null)
            throw new ArgumentException("Match not found");

        return mapper.Map<MatchResponse>(match);
    }
}