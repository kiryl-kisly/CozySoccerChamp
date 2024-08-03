using CozySoccerChamp.Application.Models.Requests.Prediction;

namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IPredictionService
{
    Task CreateAsync(PredictionRequest request);
    Task UpdateAsync(PredictionRequest request);
}