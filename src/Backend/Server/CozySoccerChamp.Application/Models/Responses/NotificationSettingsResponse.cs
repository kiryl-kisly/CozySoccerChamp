namespace CozySoccerChamp.Application.Models.Responses;

public record NotificationSettingsResponse(
    bool IsAnnouncement,
    bool IsReminder,
    int ReminderInterval,
    bool IsForceReminder);