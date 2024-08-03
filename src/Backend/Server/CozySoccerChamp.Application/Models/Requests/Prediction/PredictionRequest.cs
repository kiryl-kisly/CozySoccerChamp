namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public class PredictionRequest
{
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public PredictionScoreRequest? Prediction { get; set; }
}