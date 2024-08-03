namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserProfileController : ControllerBase
{
    /// <summary>
    ///     Изменить отображаемый никнейм пользователя в профиле
    /// </summary>
    /// <param name="userId"> UserId пользователя </param>
    /// <param name="username"> Новый username пользователя </param>
    /// <param name="userService"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserResponse> ChangeUsername(int userId, string username, [FromServices] IUserService userService)
    {
        return await userService.ChangeUsernameAsync(userId, username);
    }
}