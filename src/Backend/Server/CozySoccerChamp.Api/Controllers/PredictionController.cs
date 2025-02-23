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
    public async Task<IActionResult> MakePrediction([FromBody] PredictionRequest request)
    {
        var telegramUserId = HttpContext.GetTelegramUserId();
        request.TelegramUserId = telegramUserId;

        var response = await predictionService.MakePredictionAsync(request);

        return Ok(response);
    }

    /// <summary>
    ///     Получить все прогнозы по матчу
    /// </summary>
    [HttpGet("{matchId}")]
    [ResponseCache(Duration = 120, VaryByHeader = nameof(HeaderNames.Accept))]
    public async Task<IActionResult> GetPredictionsByMatchId([FromRoute] int matchId)
    {
        var response = await predictionService.GetByMatchIdAsync(matchId);

        return Ok(response);
    }

    /// <summary>
    ///     Получить все прогнозы игрока
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetPredictions()
    {
        var telegramUserId = HttpContext.GetTelegramUserId();

        var response = await predictionService.GetAllByUserIdAsync(telegramUserId);

        return Ok(response);
    }
}