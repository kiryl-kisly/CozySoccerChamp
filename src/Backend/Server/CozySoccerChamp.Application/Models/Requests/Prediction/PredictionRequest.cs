namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public record PredictionRequest
{
    public int MatchId { get; init; }
    public PredictionScoreRequest? Prediction { get; init; }
}