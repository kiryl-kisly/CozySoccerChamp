using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Services.Abstractions;

public interface ICompetitionService
{
    Task<CompetitionResponse> GetById(int competitionId);
}