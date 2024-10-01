using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class Prediction : BaseEntity
{
    public long TelegramUserId { get; set; }
    public int MatchId { get; set; }
    public int? PredictedHomeScore { get; set; }
    public int? PredictedAwayScore { get; set; }
    public DateTime PredictionTime { get; set; } = DateTime.UtcNow;
    
    public int? PointPerMatch { get; set; }
    public double? Coefficient { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual Match Match { get; set; }
}