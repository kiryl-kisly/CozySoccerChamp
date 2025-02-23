using CozySoccerChamp.Domain.Entities.User;
using Quartz;
using Telegram.Bot.Exceptions;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

[DisallowConcurrentExecution]
public sealed class ReminderNotificationJob(
    IApplicationUserRepository userRepository,
    IMatchRepository matchRepository,
    IPredictionRepository predictionRepository,
    INotificationSettingsRepository notificationSettingsRepository,
    ITelegramBotClient botClient,
    ILogger<ReminderNotificationJob> logger) : BaseJob(logger)
{
    protected override async Task ExecuteAsync(IJobExecutionContext context)
    {
        var now = DateTime.UtcNow;

        var upcomingMatches = await GetUpcomingMatchesAsync(now);
        if (upcomingMatches.Count == 0)
        {
            logger.LogInformation("No upcoming matches found for reminders.");
            return;
        }

        var usersWithSettings = await GetUsersWithRemindersEnabledAsync();
        if (usersWithSettings.Count == 0)
        {
            logger.LogInformation("No users to notify.");
            return;
        }

        var predictionsDict = await GetUserPredictionsAsync(usersWithSettings, upcomingMatches);
        await NotifyUsersAsync(usersWithSettings, upcomingMatches, predictionsDict, now);
    }

    #region Private Methods

    private async Task<IReadOnlyCollection<Match>> GetUpcomingMatchesAsync(DateTime now)
    {
        return await matchRepository.GetAllAsQueryable(asNoTracking: true, x => x.TeamHome, x => x.TeamAway)
            .Where(x => x.MatchTime.Date == now.Date && x.MatchResult.Status == MatchResultStatus.Timed)
            .ToListAsync();
    }

    private async Task<IReadOnlyCollection<ApplicationUser>> GetUsersWithRemindersEnabledAsync()
    {
        return await userRepository.GetAllAsQueryable(includes: x => x.Profile.NotificationSettings)
            .Where(u => u.Profile != null && u.Profile.NotificationSettings.IsReminder)
            .ToListAsync();
    }

    private async Task<Dictionary<long, IReadOnlyCollection<Prediction>>> GetUserPredictionsAsync(
        IReadOnlyCollection<ApplicationUser> users,
        IReadOnlyCollection<Match> matches)
    {
        var userIds = users.Select(u => u.TelegramUserId).ToHashSet();
        var matchIds = matches.Select(m => m.Id).ToHashSet();

        return await predictionRepository.GetAllAsQueryable()
            .Where(p => userIds.Contains(p.TelegramUserId) && matchIds.Contains(p.MatchId))
            .GroupBy(p => p.TelegramUserId)
            .ToDictionaryAsync(g => g.Key, IReadOnlyCollection<Prediction> (g) => g.ToList());
    }

    private async Task NotifyUsersAsync(
        IReadOnlyCollection<ApplicationUser> users,
        IReadOnlyCollection<Match> upcomingMatches,
        Dictionary<long, IReadOnlyCollection<Prediction>> predictionsDict,
        DateTime now)
    {
        var userNotifyCount = 0;
        var settingsToUpdate = new List<NotificationSettings>();

        foreach (var user in users)
        {
            var settings = user.Profile.NotificationSettings;
            var interval = settings.ReminderInterval;
            var lastNotified = settings.LastReminderNotified;

            if (lastNotified.HasValue && now < lastNotified.Value.AddMinutes(interval))
                continue;

            var userMatches = upcomingMatches
                .Where(m => m.MatchTime <= now.AddMinutes(interval))
                .ToList();

            if (userMatches.Count == 0)
                continue;

            var userPredictions = predictionsDict.TryGetValue(user.TelegramUserId, out var predictions)
                ? predictions
                : [];

            try
            {
                await SendNotificationAsync(user, userMatches, userPredictions);
                settings.LastReminderNotified = now;
                settingsToUpdate.Add(settings);

                userNotifyCount++;
            }
            catch (ApiRequestException ex) when (ex.ErrorCode == 403)
            {
                logger.LogWarning("User {UserId} blocked the bot. Disabling reminders.", user.TelegramUserId);
                settings.IsReminder = false;
                settingsToUpdate.Add(settings);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send notification to user {UserId}", user.TelegramUserId);
            }
        }

        if (settingsToUpdate.Count > 0)
            await UpdateLastReminderNotifiedAsync(settingsToUpdate);
        
        logger.LogInformation("Reminder notifications sent to {UserCount} users.", userNotifyCount);
    }

    private async Task UpdateLastReminderNotifiedAsync(List<NotificationSettings> settingsToUpdate)
    {
        await notificationSettingsRepository.UpdateRangeAsync(settingsToUpdate);
    }

    private async Task SendNotificationAsync(
        ApplicationUser user,
        IReadOnlyCollection<Match> matches,
        IReadOnlyCollection<Prediction> userPredictions)
    {
        var interval = user.Profile.NotificationSettings.ReminderInterval;
        var message = BuildNotificationMessage(matches, userPredictions, interval);

        await botClient.SendMessage(
            chatId: user.TelegramUserId,
            text: message,
            parseMode: ParseMode.Html);
    }

    private static string BuildNotificationMessage(
        IReadOnlyCollection<Match> matches,
        IReadOnlyCollection<Prediction> userPredictions,
        int interval)
    {
        var hasMissingPredictions = matches.Count != userPredictions.Count;

        var missingPredictionMatchIds = matches
            .Where(match => userPredictions.All(prediction => prediction.MatchId != match.Id))
            .Select(match => match.Id)
            .ToList();

        var header = hasMissingPredictions
            ? $"⚠️ <b>Last call</b>\n{interval} minutes left to lock in your predictions!"
            : "👀 <b>Check your predictions!</b>\nThe clock is ticking...";

        var matchesList = hasMissingPredictions
            ? string.Join("\n", matches.Where(match => missingPredictionMatchIds.Contains(match.Id))
                .Select(match => $"⚽ <b>{match.TeamHome.ShortName}</b> vs <b>{match.TeamAway.ShortName}</b>"))
            : string.Join("\n", matches.Select(match => $"⚽ <b>{match.TeamHome.ShortName}</b> vs <b>{match.TeamAway.ShortName}</b> "
                                                        + $"({userPredictions.FirstOrDefault(x => x.MatchId == match.Id)!.PredictedHomeScore}"
                                                        + $"-"
                                                        + $"{userPredictions.FirstOrDefault(x => x.MatchId == match.Id)!.PredictedAwayScore})"));

        var footer = hasMissingPredictions
            ? "🔥 <b>Don’t leave points on the leaderboard!</b>\n⏳ Hurry up and make your predictions before it’s too late!"
            : "📝 <b>All set?</b>\n🔁 This is your final chance to adjust your predictions before the game begins!";

        return $"{header}\n\n{matchesList}\n\n{footer}";
    }

    #endregion
}