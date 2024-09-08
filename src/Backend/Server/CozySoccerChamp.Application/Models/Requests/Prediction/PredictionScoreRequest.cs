namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public record PredictionScoreRequest
{
    public int PredictedHomeScore { get; init; }
    public int PredictedAwayScore { get; init; }
}