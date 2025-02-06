using CozySoccerChamp.Domain.Enums;

namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public record MatchResultResponse
{
    public int MatchResultId { get; init; }
    
    public DurationStatus Duration { get; init; } = DurationStatus.Regular;
    public MatchResultStatus Status { get; init; } = MatchResultStatus.Timed;
    public ScoreResponse? FullTime { get; init; }
    public ScoreResponse? HalfTime { get; init; }
    public ScoreResponse? RegularTime { get; init; }
    public ScoreResponse? ExtraTime { get; init; }
    public ScoreResponse? Penalties { get; init; }
}