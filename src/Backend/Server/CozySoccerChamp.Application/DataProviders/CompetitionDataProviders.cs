using CozySoccerChamp.Application.DataProviders.Abstractions;

namespace CozySoccerChamp.Application.DataProviders;

public sealed class CompetitionDataProvider(ICompetitionService competitionService) : IResponseDataProvider
{
    public async Task<Response> EnrichResponseAsync(Response response, long telegramUserId)
    {
        // TODO: пока хардкодом айдишник 1, в будущем будут добавляться другие competition и передаваться от клиента
        var competition = await competitionService.GetById(1);

        return response with { Competition = competition };
    }
}