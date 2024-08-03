namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public class MatchResponse
{
    public int MatchId { get; set; }

    public DateTime StartTimeUtc { get; set; }
    public char Group { get; set; }
    public string Stage { get; set; } = string.Empty;
    public int? MatchDay { get; set; }
    public int CompetitionId { get; set; }
    public TeamResponse? TeamHome { get; set; }
    public TeamResponse? TeamAway { get; set; }
    public MatchResultResponse? MatchResult { get; set; }
}