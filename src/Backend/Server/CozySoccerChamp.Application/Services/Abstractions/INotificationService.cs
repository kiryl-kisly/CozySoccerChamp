namespace CozySoccerChamp.Application.Services.Abstractions;

public interface INotificationService
{
    Task<NotificationSettingsResponse> UpdateAsync(long telegramUserId, NotificationSettingsRequest request);
}