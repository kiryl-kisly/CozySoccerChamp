using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Application.Services;

public class NotificationService(INotificationSettingsRepository notificationSettingsRepository, IMapper mapper) : INotificationService
{
    // TODO: Может вынести в appsettings.json
    private static readonly int[] Intervals = [15, 30, 60];

    public async Task<NotificationSettingsResponse> UpdateAsync(long telegramUserId, NotificationSettingsRequest request)
    {
        if (!Intervals.Contains(request.ReminderInterval))
            throw new ArgumentException($"Not valid interval: {request.ReminderInterval}");

        var settings = await notificationSettingsRepository.FindAsync(x => x.TelegramUserId == telegramUserId);
        if (settings is null)
            throw new ArgumentException($"{nameof(settings)} not found");

        settings.IsAnnouncement = request.IsAnnouncement;
        settings.IsReminder = request.IsReminder;
        settings.IsForceReminder = request.IsForceReminder;
        settings.ReminderInterval = request.ReminderInterval;

        var result = await notificationSettingsRepository.UpdateAsync(settings);

        return mapper.Map<NotificationSettingsResponse>(result);
    }
}