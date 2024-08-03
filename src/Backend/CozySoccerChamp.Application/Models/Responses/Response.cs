using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Models.Responses;

public class Response
{
    public CompetitionResponse? Competition { get; set; }
    public IReadOnlyCollection<MatchResponse>? Matches { get; set; }
}