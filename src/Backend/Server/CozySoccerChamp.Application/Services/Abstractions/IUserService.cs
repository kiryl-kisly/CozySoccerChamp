namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IUserService
{
    Task<UserProfileResponse> GetByUserId(long telegramUserId);
    Task<UserProfileResponse> CreateOrGetAsync(Update update);
    Task<UserProfileResponse> ChangeUsernameAsync(long telegramUserId, string newUserName);
}