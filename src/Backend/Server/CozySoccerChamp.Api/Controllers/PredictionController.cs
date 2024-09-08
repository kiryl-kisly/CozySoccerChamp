namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class PredictionController(IPredictionService predictionService) : ControllerBase
{
    /// <summary>
    ///     Сделать прогноз на матч
    /// </summary>
    /// <param name="request"> Запрос на создание предикта </param>
    [HttpPost]
    public async Task<PredictionResponse> MakePrediction([FromBody] PredictionRequest request)
    {
        return await predictionService.MakePredictionAsync(request);
    }
}