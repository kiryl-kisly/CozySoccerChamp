namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public record PredictionResponse
{
    public int MatchId { get; init; }
    public int? PredictedHomeScore { get; init; }
    public int? PredictedAwayScore { get; init; }
    public int? PointPerMatch { get; init; }
    public double? Coefficient { get; init; }
    public UserProfileResponse? User { get; init; }
}