namespace CozySoccerChamp.Application.Models.Responses.Soccer;

public class TeamResponse
{
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string CodeName { get; set; } = string.Empty;
    public string EmblemUrl { get; set; } = string.Empty;
}