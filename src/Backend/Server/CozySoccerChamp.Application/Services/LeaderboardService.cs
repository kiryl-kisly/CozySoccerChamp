using Microsoft.EntityFrameworkCore;

namespace CozySoccerChamp.Application.Services;

public class LeaderboardService(IPredictionRepository predictionRepository) : ILeaderboardService
{
    public async Task<IReadOnlyCollection<LeaderboardResponse>> GetLeaderboardAsync()
    {
        var leaderboardData = await predictionRepository
            .GetAllAsQueryable(asNoTracking: true, includes: x => x.User)
            .GroupBy(x => x.User.TelegramUserId)
            .Select(g => new
            {
                TelegramUserId = g.Key,
                UserName = g.FirstOrDefault()!.User.UserName,
                Points = g.Sum(x => x.PointPerMatch * x.Coefficient)
            })
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.TelegramUserId)
            .ToListAsync();

        var leaderboardWithPlaces = leaderboardData
            .Select((x, index) => new LeaderboardResponse
            {
                TelegramUserId = x.TelegramUserId,
                UserName = x.UserName,
                Points = x.Points,
                Place = index + 1
            })
            .ToList();

        return leaderboardWithPlaces;
    }

    public async Task<LeaderboardResponse?> GetLeaderboardByUserIdAsync(long telegramUserId)
    {
        var leaderboardData = await predictionRepository
            .GetAllAsQueryable(asNoTracking: true, includes: x => x.User)
            .GroupBy(x => x.User.TelegramUserId)
            .Select(g => new
            {
                TelegramUserId = g.Key,
                UserName = g.FirstOrDefault()!.User.UserName,
                Points = g.Sum(x => x.PointPerMatch * x.Coefficient)
            })
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.TelegramUserId)
            .ToListAsync();

        var leaderboardWithPlaces = leaderboardData
            .Select((x, index) => new { Data = x, Place = index + 1 })
            .Where(x => x.Data.TelegramUserId == telegramUserId)
            .Select(x => new LeaderboardResponse
            {
                TelegramUserId = x.Data.TelegramUserId,
                UserName = x.Data.UserName,
                Points = x.Data.Points,
                Place = x.Place
            })
            .FirstOrDefault();

        return leaderboardWithPlaces;
    }
}