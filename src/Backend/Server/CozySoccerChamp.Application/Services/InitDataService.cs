using CozySoccerChamp.Application.DataProviders.Abstractions;

namespace CozySoccerChamp.Application.Services;

public class InitDataService(IEnumerable<IResponseDataProvider> providers) : IInitDataService
{
    public async Task<Response> GetInitDataAsync(long telegramUserId)
    {
        var response = new Response(null, null, null, null, null);

        foreach (var provider in providers)
        {
            response = await provider.EnrichResponseAsync(response, telegramUserId);
        }

        return response;
    }
}