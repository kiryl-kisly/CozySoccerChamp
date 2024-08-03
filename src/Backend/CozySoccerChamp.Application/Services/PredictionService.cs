using CozySoccerChamp.Application.Models.Requests.Prediction;
using CozySoccerChamp.Domain.Entities.Soccer;

namespace CozySoccerChamp.Application.Services;

public class PredictionService(
    IMatchRepository matchRepository,
    IPredictionRepository predictionRepository,
    IMapper mapper) : IPredictionService
{
    private const int PredictionClosedInMinutes = 5;

    public async Task CreateAsync(PredictionRequest request)
    {
        await ValidateRequest(request);

        var prediction = mapper.Map<Prediction>(request);
        await predictionRepository.AddAsync(prediction);
    }

    public async Task UpdateAsync(PredictionRequest request)
    {
        await ValidateRequest(request);
        
        var prediction = await predictionRepository.FindAsync(x => x.UserId == request.UserId && x.MatchId == request.MatchId);
        if (prediction is null)
            throw new ArgumentException($"{nameof(Prediction)} not found");

        prediction.PredictedHomeScore = request.Prediction!.PredictedHomeScore;
        prediction.PredictedAwayScore = request.Prediction!.PredictedAwayScore;
        prediction.PredictionTime = DateTime.UtcNow;
        
        await predictionRepository.UpdateAsync(prediction);
    }

    private async Task ValidateRequest(PredictionRequest request)
    {
        var match = await matchRepository.GetByIdAsync(request.MatchId);

        if (match is null)
            throw new ArgumentException($"{nameof(Match)} not found");

        if (!IsCanPredicted(match))
            throw new InvalidOperationException("Prediction is closed");
    }

    private static bool IsCanPredicted(Match match) =>
        match.MatchTime.AddMinutes(-PredictionClosedInMinutes) >= DateTime.UtcNow;
}