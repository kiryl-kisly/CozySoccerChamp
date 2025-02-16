namespace CozySoccerChamp.Application.Services.Abstractions;

public interface ILeaderboardService
{
    Task<IReadOnlyCollection<LeaderboardResponse>> GetLeaderboardAsync();
    Task<LeaderboardResponse?> GetLeaderboardByUserIdAsync(long telegramUserId);
}