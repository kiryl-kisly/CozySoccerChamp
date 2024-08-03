using CozySoccerChamp.Domain.Enums;

namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public class MatchResultResponse
{
    public int MatchResultId { get; set; }
    
    public DurationStatus Duration { get; set; } = DurationStatus.Regular;
    public MatchResultStatus Status { get; set; } = MatchResultStatus.Timed;
    public ScoreResponse? FullTime { get; set; }
    public ScoreResponse? HalfTime { get; set; }
    public ScoreResponse? RegularTime { get; set; }
    public ScoreResponse? ExtraTime { get; set; }
    public ScoreResponse? Penalties { get; set; }
}