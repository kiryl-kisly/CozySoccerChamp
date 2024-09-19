using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class Team : BaseEntity
{
    public int ExternalTeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string CodeName { get; set; } = string.Empty;
    public string EmblemUrl { get; set; } = string.Empty;

    public virtual ICollection<Match> HomeMatches { get; set; }
    public virtual ICollection<Match> AwayMatches { get; set; }
}