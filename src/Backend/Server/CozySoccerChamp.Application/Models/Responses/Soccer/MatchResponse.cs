namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public record MatchResponse
{
    public int MatchId { get; init; }

    public DateTime StartTimeUtc { get; init; }
    public char Group { get; init; }
    public string Stage { get; init; } = string.Empty;
    public int? MatchDay { get; init; }
    public int CompetitionId { get; init; }
    public TeamResponse? TeamHome { get; init; }
    public TeamResponse? TeamAway { get; init; }
    public MatchResultResponse? MatchResult { get; init; }
}