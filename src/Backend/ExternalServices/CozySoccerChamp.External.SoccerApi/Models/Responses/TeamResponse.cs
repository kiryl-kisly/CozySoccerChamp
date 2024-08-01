using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class TeamResponse
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("shortName")]
    public string ShortName { get; set; }

    [JsonProperty("tla")]
    public string CodeName { get; set; }

    [JsonProperty("crest")]
    public string EmblemUrl { get; set; }
}