using CozySoccerChamp.Domain.Entities.Common;
using CozySoccerChamp.Domain.Enums;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class MatchResult : BaseEntity
{
    public int MatchId { get; set; }
    public DurationStatus Duration { get; set; } = DurationStatus.Regular;
    public MatchResultStatus Status { get; set; } = MatchResultStatus.Timed;
    public string FullTime { get; set; }
    public string HalfTime { get; set; }
    public string RegularTime { get; set; }
    public string ExtraTime { get; set; }
    public string Penalties { get; set; }

    public virtual Match Match { get; set; }
}