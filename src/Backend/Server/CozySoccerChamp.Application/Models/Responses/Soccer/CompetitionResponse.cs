namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public class CompetitionResponse
{
    public string? Name { get; set; } = string.Empty;
    public string? EmblemUrl { get; set; } = string.Empty;
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
}