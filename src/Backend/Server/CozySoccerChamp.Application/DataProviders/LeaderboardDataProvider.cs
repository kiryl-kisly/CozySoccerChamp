using CozySoccerChamp.Application.DataProviders.Abstractions;

namespace CozySoccerChamp.Application.DataProviders;

public sealed class LeaderboardDataProvider(ILeaderboardService leaderboardService) : IResponseDataProvider, IUserProfileDataProvider
{
    public async Task<Response> EnrichResponseAsync(Response response, long telegramUserId)
    {
        var leaderboard = await leaderboardService.GetLeaderboardAsync();
        
        return response with { Leaderboard = leaderboard };
    }

    public async Task<UserProfileResponse> EnrichUserProfileAsync(UserProfileResponse response, long telegramUserId)
    {
        var leaderboardForUser = await leaderboardService.GetLeaderboardByUserIdAsync(telegramUserId);

        if (leaderboardForUser is not null)
            response = response with { Place = leaderboardForUser.Place, Points = leaderboardForUser.Points };

        return response;
    }
}