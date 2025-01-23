using CozySoccerChamp.Infrastructure.Filters;
using Telegram.Bot.Types;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
[TypeFilter(typeof(ValidationTelegramRequestFilter))]
public class TelegramBotController : ControllerBase
{
    /// <summary>
    ///     Принимает сообщения от телеграмм бота
    /// </summary>
    /// <param name="update"> Сообщение от телеграмм бота </param>
    /// <param name="handler"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] Update update, [FromServices] ITelegramHandler handler)
    {
        await handler.HandleUpdateAsync(update);

        return Ok();
    }
}