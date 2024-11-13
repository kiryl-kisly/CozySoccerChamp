namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IUserService
{
    Task<UserResponse> GetUserByTelegramId(long telegramUserId);
    Task<UserResponse> CreateOrGetAsync(Update update);
    Task<UserResponse> ChangeUsernameAsync(long telegramUserId, string newUserName);
}