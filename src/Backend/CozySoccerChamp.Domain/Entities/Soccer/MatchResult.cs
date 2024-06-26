using CozySoccerChamp.Domain.Entities.Common;
using CozySoccerChamp.Domain.Enums;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class MatchResult : BaseEntity
{
    public int MatchId { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
    public MatchResultStatus Status { get; set; } = MatchResultStatus.Timed;

    public virtual Match Match { get; set; }
}