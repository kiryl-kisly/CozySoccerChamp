using CozySoccerChamp.Application.Models.Requests.Prediction;
using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IPredictionService
{
    Task<PredictionResponse> MakePredictionAsync(PredictionRequest request);
    Task<IReadOnlyCollection<PredictionResponse>> GetAllByUserIdAsync(long telegramUserId);
    Task<IReadOnlyCollection<PredictionResponse>> GetPredictionByMatchIdAsync(int matchId);
}