namespace CozySoccerChamp.Application.DataProviders.Abstractions;

public interface IUserProfileDataProvider
{
    Task<UserProfileResponse> EnrichUserProfileAsync(UserProfileResponse response, long telegramUserId);
}