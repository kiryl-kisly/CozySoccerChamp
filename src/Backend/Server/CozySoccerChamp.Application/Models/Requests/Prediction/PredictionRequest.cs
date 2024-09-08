namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public record PredictionRequest
{
    public int UserId { get; init; }
    public int MatchId { get; init; }
    public PredictionScoreRequest? Prediction { get; init; }
}