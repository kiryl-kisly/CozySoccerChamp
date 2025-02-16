using Microsoft.Net.Http.Headers;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class LeaderboardController(ILeaderboardService leaderboardService): ControllerBase
{
    /// <summary>
    ///     Получить информацию о лидерборде участников
    /// </summary>
    [HttpGet]
    [ResponseCache(Duration = 120, VaryByHeader = nameof(HeaderNames.Accept))]
    public async Task<IActionResult> Get()
    {
        var response = await leaderboardService.GetAsync();
        
        return Ok(response);
    }
}