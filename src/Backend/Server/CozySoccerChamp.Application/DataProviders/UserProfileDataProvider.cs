using CozySoccerChamp.Application.DataProviders.Abstractions;

namespace CozySoccerChamp.Application.DataProviders;

public sealed class UserProfileDataProvider(IUserService userService) : IResponseDataProvider
{
    public async Task<Response> EnrichResponseAsync(Response response, long telegramUserId)
    {
        var userProfile = await userService.GetFullUserByTelegramId(telegramUserId);

        return response with { UserProfile = userProfile };
    }
}