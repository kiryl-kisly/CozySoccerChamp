using CozySoccerChamp.Domain.Entities.Common;
using CozySoccerChamp.Domain.Entities.Soccer;

namespace CozySoccerChamp.Domain.Entities;

public class ApplicationUser : BaseEntity
{
    public long TelegramUserId { get; set; }
    public string TelegramUserName { get; set; } = string.Empty;
    public string TelegramFirstName { get; set; } = string.Empty;
    public string TelegramLastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
    
    public virtual ICollection<Prediction> Predictions { get; set; }
}