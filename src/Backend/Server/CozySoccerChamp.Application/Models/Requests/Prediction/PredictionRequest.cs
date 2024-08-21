namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public class PredictionRequest
{
    public int UserId { get; init; }
    public int MatchId { get; init; }
    public PredictionScoreRequest? Prediction { get; init; }
}