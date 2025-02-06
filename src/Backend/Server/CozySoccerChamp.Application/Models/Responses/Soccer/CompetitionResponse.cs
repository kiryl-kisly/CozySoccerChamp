namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public record CompetitionResponse
{
    public string? Name { get; init; } = string.Empty;
    public string? EmblemUrl { get; init; } = string.Empty;
    public DateTime Started { get; init; }
    public DateTime Finished { get; init; }
}