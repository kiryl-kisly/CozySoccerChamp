using CozySoccerChamp.Application.Models.Requests.Prediction;
using CozySoccerChamp.Application.Models.Responses.Soccer;
using CozySoccerChamp.Domain.Entities.Soccer;
using CozySoccerChamp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CozySoccerChamp.Application.Services;

public class PredictionService(
    IMatchRepository matchRepository,
    IPredictionRepository predictionRepository,
    IMapper mapper) : IPredictionService
{
    private const int PredictionClosedInMinutes = 5;

    public async Task<PredictionResponse> MakePredictionAsync(PredictionRequest request)
    {
        await ValidateRequest(request);

        var prediction = await predictionRepository.FindAsync(x => x.UserId == request.UserId && x.MatchId == request.MatchId);

        if (prediction is null)
        {
            var predictionEntity = mapper.Map<Prediction>(request);
            var result = await predictionRepository.AddAsync(predictionEntity);

            return mapper.Map<PredictionResponse>(result);
        }
        else
        {
            if (prediction.PredictedHomeScore == request.Prediction!.PredictedHomeScore
                && prediction.PredictedAwayScore == request.Prediction!.PredictedAwayScore)
                return mapper.Map<PredictionResponse>(prediction);

            prediction.PredictedHomeScore = request.Prediction!.PredictedHomeScore;
            prediction.PredictedAwayScore = request.Prediction!.PredictedAwayScore;
            prediction.PredictionTime = DateTime.UtcNow;

            var result = await predictionRepository.UpdateAsync(prediction);

            return mapper.Map<PredictionResponse>(result);
        }
    }

    public async Task<IReadOnlyCollection<PredictionResponse>> GetAllByUserIdAsync(int userId)
    {
        var predictions = await predictionRepository.GetAllAsQueryable(asNoTracking: true)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return mapper.Map<IReadOnlyCollection<PredictionResponse>>(predictions);
    }

    private async Task ValidateRequest(PredictionRequest request)
    {
        var match = await matchRepository.GetByIdAsync(request.MatchId,
            x => x.MatchResult);

        if (match is null)
            throw new ArgumentException($"{nameof(Match)} not found");

        if (!IsCanPredicted(match))
            throw new InvalidOperationException("Prediction is closed");

        if (match.MatchResult.Status != MatchResultStatus.Timed)
            throw new InvalidOperationException("Prediction is closed");
    }

    private static bool IsCanPredicted(Match match) =>
        match.MatchTime.AddMinutes(-PredictionClosedInMinutes) >= DateTime.UtcNow;
}