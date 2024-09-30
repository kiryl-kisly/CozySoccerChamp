namespace CozySoccerChamp.Api.Headers;

public class TelegramHeader
{
    [FromHeader(Name = "X-Telegram-User-Id")]
    public int UserId { get; set; }
}