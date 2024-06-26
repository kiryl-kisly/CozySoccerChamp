using Microsoft.AspNetCore.Mvc;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MatchController : ControllerBase
{
    /// <summary>
    /// Получить список всех матчей
    /// </summary>
    [HttpGet]
    public Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить информацию по матчу
    /// </summary>
    /// <param name="matchId"> Уникальный идентификатор матча </param>
    [HttpGet("/{matchId}")]
    public Task<IActionResult> Get([FromRoute] int matchId)
    {
        throw new NotImplementedException();
    }
}