using CozySoccerChamp.Infrastructure.Extensions;
using CozySoccerChamp.Infrastructure.Filters;
using Microsoft.Net.Http.Headers;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
[TypeFilter(typeof(AuthenticationTelegramRequestFilter))]
public class PredictionController(IPredictionService predictionService) : ControllerBase
{
    /// <summary>
    ///     Сделать прогноз на матч
    /// </summary>
    /// <param name="request"> Запрос на создание предикта </param>
    [HttpPost]
    public async Task<PredictionResponse> MakePrediction([FromBody] PredictionRequest request)
    {
        var telegramUserId = HttpContext.GetTelegramUserId();
        request.TelegramUserId = telegramUserId;
        
        return await predictionService.MakePredictionAsync(request);
    }

    /// <summary>
    ///     Получить все прогнозы по матчу
    /// </summary>
    [HttpGet("{matchId}")]
    [ResponseCache(Duration = 120, VaryByHeader = nameof(HeaderNames.Accept))]
    public async Task<IReadOnlyCollection<PredictionResponse>> GetPredictionsByMatchId([FromRoute] int matchId)
    {
        return await predictionService.GetPredictionByMatchIdAsync(matchId);
    }
}