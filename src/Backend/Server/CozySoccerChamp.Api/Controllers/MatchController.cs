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
    public async Task<IReadOnlyCollection<MatchResponse>> GetAll()
    {
        return await matchService.GetAllAsync();
    }

    /// <summary>
    ///     Получить информацию по матчу
    /// </summary>
    /// <param name="matchId"> Уникальный идентификатор матча </param>
    [HttpGet("{matchId}")]
    public async Task<MatchResponse> Get([FromRoute] int matchId)
    {
        return await matchService.GetByIdAsync(matchId);
    }
    
    /// <summary>
    ///     Получить информацию по начатым или завершенным матчам
    /// </summary>
    [HttpGet]
    [ResponseCache(Duration = 120, VaryByHeader = nameof(HeaderNames.Accept))]
    public async Task<IReadOnlyCollection<MatchResponse>> GetStartedOrFinished()
    {
        return await matchService.GetStartedOrFinishedAsync();
    }
}