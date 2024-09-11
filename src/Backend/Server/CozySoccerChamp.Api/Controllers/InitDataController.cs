namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class InitDataController(
    ICompetitionService competitionService,
    IMatchService matchService,
    IUserService userService, 
    IPredictionService predictionService) : ControllerBase
{
    /// <summary>
    ///     Получить всё информацию при первоночальной загрузке веб-приложения
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int userId)
    {
        var competition = await competitionService.GetById(1);
        var userProfile = await userService.GetUserById(userId);
        var matches = await matchService.GetAllAsync();
        var predictions = await predictionService.GetAllByUserIdAsync(userId);
        var leaderboard = await predictionService.GetLeaderboardAsync();

        var response = new Response
        {
            Competition = competition,
            UserProfile = userProfile,
            Matches = matches,
            Predictions = predictions,
            Leaderboard = leaderboard,
        };

        return Ok(response);
    }
}