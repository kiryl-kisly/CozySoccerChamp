using CozySoccerChamp.Application.DataProviders.Abstractions;

namespace CozySoccerChamp.Application.DataProviders;

public sealed class PredictionDataProvider(IPredictionService predictionService) : IResponseDataProvider
{
    public async Task<Response> EnrichResponseAsync(Response response, long telegramUserId)
    {
        var predictions = await predictionService.GetAllByUserIdAsync(telegramUserId);

        return response with { Predictions = predictions };
    }
}