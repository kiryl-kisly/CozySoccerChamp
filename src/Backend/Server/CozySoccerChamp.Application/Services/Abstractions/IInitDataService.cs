namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IInitDataService
{
    Task<Response> GetInitDataAsync(long telegramUserId);
}