namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class InitDataController(IMatchService matchService, IUserService userService) : ControllerBase
{
    /// <summary>
    ///     Получить всё информацию при первоночальной загрузке веб-приложения
    /// </summary>
    [HttpGet]
    public async Task<Response> Get([FromQuery] int userId)
    {
        var userProfile = await userService.GetUserById(userId);
        var matches = await matchService.GetAllAsync();

        var response = new Response
        {
            UserProfile = userProfile,
            Matches = matches
        };

        return response;
    }
}