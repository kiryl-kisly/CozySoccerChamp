using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class Competition
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("emblem")]
    public string EmblemUrl { get; set; }
}