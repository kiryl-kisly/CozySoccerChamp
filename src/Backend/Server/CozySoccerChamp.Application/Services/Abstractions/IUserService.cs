namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IUserService
{
    Task<UserResponse> CreateOrGetAsync(Update update);
    Task<UserResponse> ChangeUsernameAsync(int userId, string username);
}