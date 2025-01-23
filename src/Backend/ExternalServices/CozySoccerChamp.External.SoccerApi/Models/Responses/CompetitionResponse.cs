using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class CompetitionResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("emblem")]
    public string EmblemUrl { get; set; }
}