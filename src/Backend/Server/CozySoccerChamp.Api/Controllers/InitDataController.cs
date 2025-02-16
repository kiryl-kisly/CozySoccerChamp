using CozySoccerChamp.Application.Services;
using CozySoccerChamp.Infrastructure.Extensions;
using CozySoccerChamp.Infrastructure.Filters;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
[TypeFilter(typeof(AuthenticationTelegramRequestFilter))]
public class InitDataController(IInitDataService initDataService) : ControllerBase
{
    /// <summary>
    ///     Получить всё информацию при первоночальной загрузке веб-приложения
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var telegramUserId = HttpContext.GetTelegramUserId();
        var response = await initDataService.GetInitDataAsync(telegramUserId);
        
        return Ok(response);
    }
}