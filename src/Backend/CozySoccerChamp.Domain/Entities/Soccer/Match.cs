using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class Match : BaseEntity
{
    public int ExternalMatchId { get; set; }
    public int? TeamHomeId { get; set; }
    public int? TeamAwayId { get; set; }
    public char? Group { get; set; }
    public string Stage { get; set; }
    public DateTime MatchTime { get; set; }
    public int? CompetitionId { get; set; }

    public virtual Team TeamHome { get; set; }
    public virtual Team TeamAway { get; set; }
    public virtual MatchResult MatchResult { get; set; }
    public virtual Competition Competition { get; set; }
    
    public virtual ICollection<Prediction> Predictions { get; set; }
}