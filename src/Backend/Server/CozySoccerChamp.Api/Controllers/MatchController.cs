using Microsoft.Net.Http.Headers;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class MatchController(IMatchService matchService) : ControllerBase
{
    /// <summary>
    ///     Получить информацию о всех матчах
    /// </summary>
    [HttpGet]
    [ResponseCache(Duration = 120, VaryByHeader = nameof(HeaderNames.Accept))]
    public async Task<IActionResult> GetAll()
    {
        var response = await matchService.GetAllAsync();

        return Ok(response);
    }

    /// <summary>
    ///     Получить информацию по матчу
    /// </summary>
    /// <param name="matchId"> Уникальный идентификатор матча </param>
    [HttpGet("{matchId}")]
    public async Task<IActionResult> Get([FromRoute] int matchId)
    {
        var response = await matchService.GetByIdAsync(matchId);

        return Ok(response);
    }

    /// <summary>
    ///     Получить информацию по начатым или завершенным матчам
    /// </summary>
    [HttpGet]
    [ResponseCache(Duration = 120, VaryByHeader = nameof(HeaderNames.Accept))]
    public async Task<IActionResult> GetStartedOrFinished()
    {
        var response = await matchService.GetStartedOrFinishedAsync();

        return Ok(response);
    }
}