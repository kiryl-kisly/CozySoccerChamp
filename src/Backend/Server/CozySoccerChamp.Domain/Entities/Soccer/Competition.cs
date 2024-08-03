using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class Competition : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string EmblemUrl { get; set; } = string.Empty;
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
    
    public virtual ICollection<Match> Matches { get; set; }
}