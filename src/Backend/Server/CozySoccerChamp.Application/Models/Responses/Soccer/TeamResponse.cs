namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public record TeamResponse
{
    public int TeamId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ShortName { get; init; } = string.Empty;
    public string CodeName { get; init; } = string.Empty;
    public string EmblemUrl { get; init; } = string.Empty;
}