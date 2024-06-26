using Microsoft.AspNetCore.Mvc;

namespace CozySoccerChamp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PredictionController : ControllerBase
{
    /// <summary>
    /// Создать новый прогноз на матч
    /// </summary>
    [HttpPost]
    public Task<IActionResult> Post(int matchId, long chatId)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Обновить прогноз на матч
    /// </summary>
    [HttpPut]
    public Task<IActionResult> Put(int matchId, long chatId)
    {
        throw new NotImplementedException();
    }
}