namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public class PredictionScoreRequest
{
    public int PredictedHomeScore { get; init; }
    public int PredictedAwayScore { get; init; }
}