using Microsoft.EntityFrameworkCore;

namespace CozySoccerChamp.Application.Services;

public class LeaderboardService(IPredictionRepository predictionRepository) : ILeaderboardService
{
    public async Task<IReadOnlyCollection<LeaderboardResponse>> GetLeaderboardAsync()
    {
        var leaderboard = await predictionRepository.GetAllAsQueryable(asNoTracking: true, includes: x => x.User)
            .GroupBy(x => x.User.TelegramUserId)
            .Select(g => new LeaderboardResponse
            {
                TelegramUserId = g.FirstOrDefault()!.TelegramUserId,
                UserName = g.FirstOrDefault()!.User.UserName,
                Points = g.Sum(x => x.PointPerMatch * x.Coefficient)
            })
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.TelegramUserId)
            .ToListAsync();

        var place = 1;

        var leaderboardWithPlaces = leaderboard
            .Select(x => new LeaderboardResponse
            {
                TelegramUserId = x.TelegramUserId,
                UserName = x.UserName,
                Points = x.Points,
                Place = place++
            })
            .ToList();

        return leaderboardWithPlaces;
    }
}