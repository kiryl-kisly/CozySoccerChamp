namespace CozySoccerChamp.Application.Models.Responses;

public class UserResponse
{
    public long TelegramUserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public bool IsEnabledNotification { get; set; }
}