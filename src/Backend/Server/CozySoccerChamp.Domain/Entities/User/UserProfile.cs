using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.User;

public class UserProfile : BaseEntity
{
    public long TelegramUserId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual NotificationSettings NotificationSettings { get; set; }
}