namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class PredictionController(IPredictionService predictionService) : ControllerBase
{
    /// <summary>
    ///     Создать новый прогноз на матч
    /// </summary>
    /// <param name="request"> Запрос на создание предикта </param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PredictionRequest request)
    {
        await predictionService.CreateAsync(request);

        return Ok(true);
    }

    /// <summary>
    ///     Обновить прогноз на матч
    /// </summary>
    /// <param name="request"> Запрос на обновление предикта </param>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PredictionRequest request)
    {
        await predictionService.UpdateAsync(request);

        return Ok(true);
    }
}