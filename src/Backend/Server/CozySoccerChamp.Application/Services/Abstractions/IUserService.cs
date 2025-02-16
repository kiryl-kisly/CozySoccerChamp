namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IUserService
{
    Task<UserProfileResponse> GetUserByTelegramId(long telegramUserId);
    Task<UserProfileResponse> GetFullUserByTelegramId(long telegramUserId);
    Task<UserProfileResponse> CreateOrGetAsync(Update update);
    Task<UserProfileResponse> ChangeUsernameAsync(long telegramUserId, string newUserName);
}