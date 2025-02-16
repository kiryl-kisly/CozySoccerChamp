namespace CozySoccerChamp.Application.Services.Abstractions;

public interface ILeaderboardService
{
    Task<IReadOnlyCollection<LeaderboardResponse>> GetAsync();
    Task<LeaderboardResponse?> GetByUserIdAsync(long telegramUserId);
}