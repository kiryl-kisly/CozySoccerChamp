using CozySoccerChamp.Application.Models.Requests;
using CozySoccerChamp.Infrastructure.Extensions;
using CozySoccerChamp.Infrastructure.Filters;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Produces("application/json")]
[Consumes("application/json")]
[TypeFilter(typeof(AuthenticationTelegramRequestFilter))]
public class SettingController(INotificationService notificationService) : ControllerBase
{
    /// <summary>
    ///     Обновить настройки уведомлений
    /// </summary>
    [HttpPut("Notifications/[action]")]
    public async Task<ActionResult<NotificationSettingsResponse>> Update([FromBody] NotificationSettingsRequest request)
    {
        var telegramUserId = HttpContext.GetTelegramUserId();
        
        var settings = await notificationService.UpdateAsync(telegramUserId, request);
        
        return Ok(settings);
    }
}