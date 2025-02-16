namespace CozySoccerChamp.Application.Services.Abstractions;

public interface INotificationService
{
    Task<NotificationSettingsResponse> GetNotificationSettingsAsync(long telegramUserId);
    Task<NotificationSettingsResponse> UpdateNotificationSettingsAsync(long telegramUserId, NotificationSettingsRequest request);
}