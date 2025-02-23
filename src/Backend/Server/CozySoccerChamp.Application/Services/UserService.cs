using CozySoccerChamp.Application.DataProviders.Abstractions;
using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Application.Services;

public class UserService(
    IApplicationUserRepository userRepository,
    IEnumerable<IUserProfileDataProvider> providers,
    IMapper mapper) : IUserService
{
    public async Task<UserProfileResponse> GetByUserId(long telegramUserId)
    {
        var userEntity = await userRepository.FindAsync(x => x.TelegramUserId == telegramUserId, includes: x => x.Profile.NotificationSettings);
        if (userEntity is null)
            throw new ArgumentException($"{nameof(User)} not found");

        var userProfile = mapper.Map<UserProfileResponse>(userEntity);

        foreach (var provider in providers)
        {
            userProfile = await provider.EnrichUserProfileAsync(userProfile, telegramUserId);
        }

        return userProfile;
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
}