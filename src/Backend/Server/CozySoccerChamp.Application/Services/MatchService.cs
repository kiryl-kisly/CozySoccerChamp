using CozySoccerChamp.Application.Models.Responses.Soccer;
using CozySoccerChamp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CozySoccerChamp.Application.Services;

public class MatchService(IMatchRepository matchRepository, IMapper mapper) : IMatchService
{
    public async Task<IReadOnlyCollection<MatchResponse>> GetAllAsync()
    {
        var matches = await matchRepository.GetAllAsQueryable(asNoTracking: true,
                x => x.TeamHome,
                x => x.TeamAway,
                x => x.MatchResult)
            .OrderBy(x => x.MatchTime)
            .ThenBy(x => x.Id)
            .ToListAsync();

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

    public async Task<IReadOnlyCollection<MatchResponse>> GetStartedOrFinishedAsync()
    {
        var matches = await matchRepository.GetAllAsQueryable(asNoTracking: true,
                x => x.TeamHome,
                x => x.TeamAway,
                x => x.MatchResult)
            .Where(x => x.MatchResult.Status == MatchResultStatus.Started || x.MatchResult.Status == MatchResultStatus.Finished)
            .OrderByDescending(x => x.MatchTime)
            .ThenByDescending(x => x.Id)
            .ToListAsync();

        return mapper.Map<IReadOnlyCollection<MatchResponse>>(matches);
    }
}