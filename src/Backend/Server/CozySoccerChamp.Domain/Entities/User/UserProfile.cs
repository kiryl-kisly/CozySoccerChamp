namespace CozySoccerChamp.Domain.Entities.User;

public class UserProfile
{
    public long TelegramUserId { get; set; }

    public bool IsEnabledNotification { get; set; }
    public DateTime? LastNotified { get; set; }

    public virtual ApplicationUser User { get; set; }
}