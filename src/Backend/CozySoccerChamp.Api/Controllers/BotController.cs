using Microsoft.AspNetCore.Mvc;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BotController : ControllerBase
{
    /// <summary>
    /// Принимает сообщения от телеграмм бота
    /// </summary>
    [HttpPost]
    public Task<IActionResult> Post()
    {
        throw new NotImplementedException();
    }
}