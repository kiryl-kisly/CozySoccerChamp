using Quartz;
using Telegram.Bot.Exceptions;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

[DisallowConcurrentExecution]
public sealed class AnnouncementNotificationJob(
    IApplicationUserRepository userRepository,
    IMatchRepository matchRepository,
    ITelegramBotClient botClient,
    ILogger<AnnouncementNotificationJob> logger) : BaseJob(logger)
{
    private const string NotificationTemplateText = """
                                                    🚀 <b>Welcome to {0}! Matchday {1} is live! 🏆</b>

                                                    🔥 <b>Upcoming matches in this matchday:</b>

                                                    {2}

                                                    📈 Score points, outplay your rivals, and dominate the leaderboard!  
                                                    🎯 Only the best predictors will rise to the top! Are you in?  
                                                    """;

    protected override async Task ExecuteAsync(IJobExecutionContext context)
    {
        var today = DateTime.UtcNow.Date;

        var (stage, matchDay) = await GetFirstMatchInfoOfTheDayAsync(today);
        if (stage is null || matchDay is null)
        {
            logger.LogInformation("No matches found for today.");
            return;
        }

        var isFirstDayOfMatchday = await IsFirstDayOfMatchdayAsync(stage, matchDay.Value, today);
        if (!isFirstDayOfMatchday)
        {
            logger.LogInformation("Today is not the first day of matchday {Matchday}.", matchDay.Value);
            return;
        }

        var matchesForMatchday = await GetMatchesForMatchdayAsync(stage, matchDay.Value);
        if (matchesForMatchday.Count == 0)
        {
            logger.LogInformation("No matches found for matchday {Matchday}.", matchDay.Value);
            return;
        }

        var userIdsToNotify = await GetUserIdsWithAnnouncementsEnabledAsync();
        if (userIdsToNotify.Count == 0)
        {
            logger.LogInformation("No users have announcements enabled.");
            return;
        }

        var message = BuildMatchdayAnnouncementMessage(stage, matchDay.Value, matchesForMatchday);

        await SendNotificationsAsync(userIdsToNotify, message);
    }

    #region private methods

    private async Task<(string? Stage, int? MatchDay)> GetFirstMatchInfoOfTheDayAsync(DateTime today)
    {
        var matchInfo = await matchRepository.GetAllAsQueryable()
            .Where(m => m.MatchTime.Date == today)
            .OrderBy(m => m.MatchTime)
            .Select(x => new
            {
                x.Stage,
                x.MatchDay
            })
            .FirstOrDefaultAsync();

        return matchInfo is not null
            ? (matchInfo.Stage, matchInfo.MatchDay)
            : (null, null);
    }

    private async Task<IReadOnlyList<long>> GetUserIdsWithAnnouncementsEnabledAsync()
    {
        var users = await userRepository.GetAllAsQueryable()
            .Where(u => u.Profile.NotificationSettings.IsAnnouncementEnabled)
            .Select(x => x.TelegramUserId)
            .ToListAsync();

        return users;
    }

    private async Task<bool> IsFirstDayOfMatchdayAsync(string stage, int matchday, DateTime today)
    {
        var isFirstDay = !await matchRepository.GetAllAsQueryable()
            .AnyAsync(m => m.Stage == stage && m.MatchDay == matchday && m.MatchTime.Date < today);

        if (!isFirstDay)
            logger.LogInformation("Matchday {Matchday} already started before {Date}.", matchday, today);

        return isFirstDay;
    }

    private async Task<IReadOnlyCollection<Match>> GetMatchesForMatchdayAsync(string stage, int matchday)
    {
        var matches = await matchRepository.GetAllAsQueryable(asNoTracking: true, x => x.TeamHome, x => x.TeamAway)
            .Where(m => m.Stage == stage && m.MatchDay == matchday)
            .OrderBy(m => m.MatchTime)
            .ToListAsync();

        logger.LogInformation("Found {MatchCount} matches for matchday {Matchday}.", matches.Count, matchday);
        return matches;
    }

    private static string BuildMatchdayAnnouncementMessage(string stage, int matchday, IReadOnlyCollection<Match> matches)
    {
        var groupedMatches = matches
            .GroupBy(x => x.MatchTime.Date)
            .OrderBy(x => x.Key)
            .Select(x => $"📅 <b>{x.Key:dd.MM.yyyy}</b>\n"
                         + string.Join("\n", x.Select(m => $"⚽ <b>{m.TeamHome.ShortName}</b> vs <b>{m.TeamAway.ShortName}</b>")));

        return string.Format(NotificationTemplateText, GetStageName(stage), matchday, string.Join("\n\n", groupedMatches));
    }

    private static string GetStageName(string stage)
    {
        return stage switch
        {
            "LEAGUE_STAGE" => "League Stage",
            "PLAYOFFS" => "Playoff Stage",
            "LAST_16" => "1/8 Stage",
            "QUARTER_FINALS" => "1/4 Stage",
            "SEMI_FINALS" => "Semifinal",
            "FINAL" => "Final",
            _ => "Unknown Stage"
        };
    }

    private async Task SendNotificationsAsync(IReadOnlyCollection<long> userIds, string message)
    {
        var userNotifyCount = 0;
        
        foreach (var userId in userIds)
        {
            try
            {
                await botClient.SendMessage(
                    chatId: userId,
                    text: message,
                    parseMode: ParseMode.Html);

                userNotifyCount++;

                // We add a delay so we don't spam the request
                await Task.Delay(100);
            }
            catch (ApiRequestException ex) when (ex.ErrorCode == 403)
            {
                logger.LogWarning("User {UserId} blocked the bot. Disabling reminders.", userId);

                var user = await userRepository.FindAsync(x => x.TelegramUserId == userId, x => x.Profile.NotificationSettings);
                
                user!.Profile.NotificationSettings.IsAnnouncementEnabled = false;
                await userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send announcement to user {UserId}", userId);
            }
        }
        
        logger.LogInformation("Announcement notifications sent to {UserCount} users.", userNotifyCount);
    }

    #endregion
}