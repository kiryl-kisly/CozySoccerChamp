namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public record ScoreResponse
{
    public int? HomeTeamScore { get; init; }

    public int? AwayTeamScore { get; init; }
}