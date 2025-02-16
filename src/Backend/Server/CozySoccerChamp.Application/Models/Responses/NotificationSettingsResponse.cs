namespace CozySoccerChamp.Application.Models.Responses;

public record NotificationSettingsResponse(
    bool IsAnnouncementEnabled,
    bool IsReminderEnabled,
    int ReminderInterval,
    bool IsForceNotificationEnabled,
    DateTime? LastReminderNotified);