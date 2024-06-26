using CozySoccerChamp.Domain.Entities.Common;

namespace CozySoccerChamp.Domain.Entities.Soccer;

public class Championship : BaseEntity
{
    public string Name { get; set; } = default!;
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
}