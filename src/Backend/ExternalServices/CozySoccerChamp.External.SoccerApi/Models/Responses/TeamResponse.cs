using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class TeamResponse
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("shortName")]
    public string ShortName { get; set; }

    [JsonPropertyName("tla")]
    public string CodeName { get; set; }

    [JsonPropertyName("crest")]
    public string EmblemUrl { get; set; }
}