using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Application.Services;

public class UserService(IApplicationUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<UserProfileResponse> GetUserByTelegramId(long telegramUserId)
    {
        var user = await userRepository.FindAsync(x => x.TelegramUserId == telegramUserId, includes: x => x.Profile);
        if (user is null)
            throw new ArgumentException($"{nameof(User)} not found");

        return mapper.Map<UserProfileResponse>(user);
    }

    public async Task<UserProfileResponse> CreateOrGetAsync(Update update)
    {
        if (update.Message?.Chat is null)
            throw new InvalidOperationException();

        var user = mapper.Map<ApplicationUser>(update.Message.Chat);

        var applicationUser = await userRepository.FindAsync(x => x.TelegramUserId == user.TelegramUserId, includes: x => x.Profile);
        if (applicationUser is not null)
            return mapper.Map<UserProfileResponse>(applicationUser);

        await userRepository.AddAsync(user);

        return mapper.Map<UserProfileResponse>(user);
    }

    public async Task<UserProfileResponse> ChangeUsernameAsync(long telegramUserId, string newUserName)
    {
        var user = await userRepository.FindAsync(x => x.TelegramUserId == telegramUserId, includes: x => x.Profile);
        if (user is null)
            throw new ArgumentException($"{nameof(User)} not found");

        user.UserName = newUserName;

        await userRepository.UpdateAsync(user);

        return mapper.Map<UserProfileResponse>(user);
    }

    public async Task<UserProfileResponse> ToggleNotificationAsync(long telegramUserId, bool isEnabled)
    {
        var user = await userRepository.FindAsync(x => x.TelegramUserId == telegramUserId, includes: x => x.Profile);
        if (user is null)
            throw new ArgumentException($"{nameof(User)} not found");

        user.Profile.IsEnabledNotification = isEnabled;

        await userRepository.UpdateAsync(user);

        return mapper.Map<UserProfileResponse>(user);
    }
}