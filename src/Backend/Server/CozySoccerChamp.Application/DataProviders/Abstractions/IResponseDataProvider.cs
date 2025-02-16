namespace CozySoccerChamp.Application.DataProviders.Abstractions;

public interface IResponseDataProvider
{
    Task<Response> EnrichResponseAsync(Response response, long telegramUserId);
}