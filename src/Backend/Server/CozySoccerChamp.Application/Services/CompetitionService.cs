using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Services;

public class CompetitionService(ICompetitionRepository competitionRepository, IMapper mapper) : ICompetitionService
{
    public async Task<CompetitionResponse> GetById(int competitionId)
    {
        var competition = await competitionRepository.GetByIdAsync(competitionId);

        if (competition is null)
            throw new ArgumentException("Competition not found");

        return mapper.Map<CompetitionResponse>(competition);
    }
}