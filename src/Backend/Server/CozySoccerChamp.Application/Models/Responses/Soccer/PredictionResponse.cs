namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public class PredictionResponse
{
    public int MatchId { get; set; }
    public int? PredictedHomeScore { get; set; }
    public int? PredictedAwayScore { get; set; }
    public int? PointPerMatch { get; set; }
    public double? Coefficient { get; set; }
    public UserResponse? User { get; set; }
}