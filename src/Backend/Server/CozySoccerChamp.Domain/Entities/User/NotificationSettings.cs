using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.User;

public class NotificationSettings : BaseEntity
{
    public long TelegramUserId { get; set; }
    public bool IsAnnouncementEnabled { get; set; } = true;
    public bool IsReminderEnabled { get; set; } = true;
    public int ReminderInterval { get; set; } = 60;
    public bool IsForceNotificationEnabled { get; set; }
    
    public DateTime? LastReminderNotified { get; set; }

    public virtual UserProfile UserProfile { get; set; }
}