namespace CozySoccerChamp.Application.Services.Abstractions;

public interface IInitDataService
{
    Task<Response> GetAsync(long telegramUserId);
}