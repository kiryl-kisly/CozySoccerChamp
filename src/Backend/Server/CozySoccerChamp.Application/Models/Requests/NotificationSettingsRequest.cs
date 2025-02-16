using System.ComponentModel.DataAnnotations;

namespace CozySoccerChamp.Application.Models.Requests;

public record NotificationSettingsRequest
{
    public bool IsAnnouncementEnabled { get; init; }
    public bool IsReminderEnabled { get; init; }
    public bool IsForceNotificationEnabled { get; init; }
    
    [Range(15, 60, ErrorMessage = "Reminder interval must be 15, 30, or 60 minutes.")]
    public int ReminderInterval { get; init; }
}