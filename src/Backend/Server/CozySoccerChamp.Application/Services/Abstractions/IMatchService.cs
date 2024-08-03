using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IMatchService
{
    Task<IReadOnlyCollection<MatchResponse>> GetAllAsync();
    Task<MatchResponse> GetByIdAsync(int matchId);
}