using Quartz;
using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

public sealed class MatchNotificationJob(
    IApplicationUserRepository userRepository,
    IMatchRepository matchRepository,
    IPredictionRepository predictionRepository,
    ITelegramBotClient botClient,
    ILogger<MatchNotificationJob> logger) : BaseJob(logger)
{
    private const string MessageToChat = "\u26bd Don't forget to make your predictions for the upcoming matches!";

    protected override async Task ExecuteAsync(IJobExecutionContext context)
    {
        var now = DateTime.UtcNow;

        var upcomingMatches = await GetUpcomingMatchesAsync(now);
        if (upcomingMatches.Count == 0)
        {
            logger.LogInformation("No upcoming matches found for notifications.");
            return;
        }

        var usersToNotify = await GetUsersToNotifyAsync(now, upcomingMatches);
        if (usersToNotify.Count == 0)
        {
            logger.LogInformation("No users to notify.");
            return;
        }

        await NotifyUsersAsync(usersToNotify, now);

        logger.LogInformation($"Notifications sent to {usersToNotify.Count} users");
    }

    #region private metods

    private async Task<List<Match>> GetUpcomingMatchesAsync(DateTime now)
    {
        return await matchRepository.GetAllAsQueryable(includes: x => x.Predictions)
            .Where(x => x.MatchTime > now && x.MatchTime <= now.AddHours(1))
            .ToListAsync();
    }

    private async Task<List<ApplicationUser>> GetUsersToNotifyAsync(DateTime now, List<Match> upcomingMatches)
    {
        var users = await userRepository.GetAllAsQueryable(includes: x => x.Profile)
            .Where(x => x.Profile.IsEnabledNotification
                        && (x.Profile.LastNotified == null || x.Profile.LastNotified < now.AddMinutes(-60)))
            .ToListAsync();

        var usersToNotify = users
            .Where(user => !upcomingMatches
                .Any(match => match.Predictions.Any(p => p.TelegramUserId == user.TelegramUserId)))
            .ToList();

        return usersToNotify;
    }

    private async Task NotifyUsersAsync(List<ApplicationUser> users, DateTime now)
    {
        foreach (var user in users)
        {
            await SendNotificationAsync(user);

            UpdateUserLastNotified(user, now);
        }

        await userRepository.UpdateRangeAsync(users);
    }

    private async Task SendNotificationAsync(ApplicationUser user)
    {
        await botClient.SendMessage(
            chatId: user.TelegramUserId,
            text: MessageToChat);
    }

    private static void UpdateUserLastNotified(ApplicationUser user, DateTime now)
    {
        user.Profile.LastNotified = now;
    }

    #endregion
}