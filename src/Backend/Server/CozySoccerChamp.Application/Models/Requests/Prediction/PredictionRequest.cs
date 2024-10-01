namespace CozySoccerChamp.Application.Models.Requests.Prediction;

public record PredictionRequest
{
    public long TelegramUserId { get; set; }
    public int MatchId { get; init; }
    public PredictionScoreRequest? Prediction { get; init; }
}