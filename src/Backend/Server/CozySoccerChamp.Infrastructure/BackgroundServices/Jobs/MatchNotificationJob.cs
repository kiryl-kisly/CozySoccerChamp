using Quartz;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

public sealed class MatchNotificationJob(
    IApplicationUserRepository userRepository,
    IMatchRepository matchRepository,
    ITelegramBotClient botClient,
    ILogger<MatchNotificationJob> logger) : BaseJob(logger)
{
    private const string MessageToChat = "\u26bd Don't forget to make your predictions for the upcoming matches!";

    protected override async Task ExecuteAsync(IJobExecutionContext context)
    {
        var now = DateTime.UtcNow;

        if (!await ShouldNotify(now))
        {
            logger.LogInformation("No upcoming matches found for notifications.");
            return;
        }

        var usersToNotify = await userRepository.GetAllAsQueryable()
            .Where(x => x.Profile.IsEnabledNotification
                        && (x.Profile.LastNotified == null || x.Profile.LastNotified < now.AddMinutes(-60)))
            .ToListAsync();

        if (usersToNotify.Count == 0)
            return;

        foreach (var user in usersToNotify)
        {
            await botClient.SendMessage(
                chatId: user.TelegramUserId,
                text: MessageToChat);

            user.Profile.LastNotified = now;
        }

        await userRepository.UpdateRangeAsync(usersToNotify);

        logger.LogInformation($"Notifications sent to {usersToNotify.Count} users");
    }

    private async Task<bool> ShouldNotify(DateTime now)
    {
        var upcomingMatches = await matchRepository.GetAllAsQueryable()
            .Where(x => x.MatchTime > now && x.MatchTime < now.AddHours(1))
            .ToListAsync();

        return upcomingMatches.Count != 0;
    }
}