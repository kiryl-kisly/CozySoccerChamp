using CozySoccerChamp.Infrastructure.Extensions;
using CozySoccerChamp.Infrastructure.Filters;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
[TypeFilter(typeof(AuthenticationTelegramRequestFilter))]
public class UserProfileController(IUserService userService) : ControllerBase
{
    /// <summary>
    ///     Изменить отображаемый никнейм пользователя в профиле
    /// </summary>
    /// <param name="newUserName"> Новый username пользователя </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserProfileResponse> ChangeUsername(string newUserName)
    {
        var telegramUserId = HttpContext.GetTelegramUserId();
        
        return await userService.ChangeUsernameAsync(telegramUserId, newUserName);
    }
}