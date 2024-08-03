namespace CozySoccerChamp.Application.Services;

public class UserService(IApplicationUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<UserResponse> CreateOrGetAsync(Update update)
    {
        if (update.Message?.Chat is null)
            throw new InvalidOperationException();

        var user = mapper.Map<ApplicationUser>(update.Message.Chat);

        var applicationUser = await userRepository.FindAsync(x => x.ChatId == user.ChatId);

        if (applicationUser is not null)
            return mapper.Map<UserResponse>(applicationUser);

        await userRepository.AddAsync(user);

        return mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse> ChangeUsernameAsync(int userId, string username)
    {
        var user = await userRepository.GetByIdAsync(userId);

        if (user is null)
            throw new ArgumentException($"{nameof(User)} not found");

        user.UserName = username;

        await userRepository.UpdateAsync(user);

        return mapper.Map<UserResponse>(user);
    }
}