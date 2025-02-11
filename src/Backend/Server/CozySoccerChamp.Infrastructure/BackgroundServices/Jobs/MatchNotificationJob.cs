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

    /// <summary>
    /// Получает список предстоящих матчей.
    /// </summary>
    private async Task<List<Match>> GetUpcomingMatchesAsync(DateTime now)
    {
        return await matchRepository.GetAllAsQueryable()
            .Where(x => x.MatchTime > now && x.MatchTime <= now.AddHours(1))
            .ToListAsync();
    }

    /// <summary>
    /// Получает список пользователей, которых нужно уведомить, исключая тех, кто уже сделал прогнозы.
    /// </summary>
    private async Task<List<ApplicationUser>> GetUsersToNotifyAsync(DateTime now, List<Match> upcomingMatches)
    {
        var users = await userRepository.GetAllAsQueryable(includes: x => x.Profile)
            .Where(x => x.Profile.IsEnabledNotification
                        && (x.Profile.LastNotified == null || x.Profile.LastNotified < now.AddMinutes(-60)))
            .ToListAsync();

        var usersToNotify = new List<ApplicationUser>();

        foreach (var user in users)
        {
            var hasPredictions = await predictionRepository.GetAllAsQueryable()
                .AnyAsync(p => p.TelegramUserId == user.Id && upcomingMatches.Select(m => m.Id).Contains(p.MatchId));

            if (!hasPredictions)
            {
                usersToNotify.Add(user);
            }
        }

        return usersToNotify;
    }

    /// <summary>
    /// Отправляет уведомления пользователям и обновляет время последнего уведомления.
    /// </summary>
    private async Task NotifyUsersAsync(List<ApplicationUser> users, DateTime now)
    {
        foreach (var user in users)
        {
            await SendNotificationAsync(user);

            UpdateUserLastNotified(user, now);
        }

        await userRepository.UpdateRangeAsync(users);
    }

    /// <summary>
    /// Отправляет уведомление пользователю.
    /// </summary>
    private async Task SendNotificationAsync(ApplicationUser user)
    {
        await botClient.SendMessage(
            chatId: user.TelegramUserId,
            text: MessageToChat);
    }

    /// <summary>
    /// Обновляет время последнего уведомления пользователя.
    /// </summary>
    private static void UpdateUserLastNotified(ApplicationUser user, DateTime now)
    {
        user.Profile.LastNotified = now;
    }

    #endregion
}