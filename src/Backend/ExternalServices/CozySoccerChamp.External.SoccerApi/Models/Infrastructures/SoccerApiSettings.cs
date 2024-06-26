namespace CozySoccerChamp.External.SoccerApi.Models.Infrastructures;

public class SoccerApiSettings
{
    public static readonly string SectionName = nameof(SoccerApiSettings);

    public string ApiToken { get; init; } = default!;
    public string BaseUrl { get; init; } = default!;
}