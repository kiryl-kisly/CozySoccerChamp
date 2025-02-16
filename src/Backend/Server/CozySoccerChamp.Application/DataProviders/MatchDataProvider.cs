using CozySoccerChamp.Application.DataProviders.Abstractions;

namespace CozySoccerChamp.Application.DataProviders;

public sealed class MatchDataProvider(IMatchService matchService) : IResponseDataProvider
{
    public async Task<Response> EnrichResponseAsync(Response response, long telegramUserId)
    {
        var matches = await matchService.GetAllAsync();
        
        return response with { Matches = matches };
    }
}