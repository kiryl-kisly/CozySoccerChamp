using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class ResultSetResponse
{
    [JsonProperty("count")]
    public int Count { get; set; }
    
    [JsonProperty("first")]
    public DateTime First { get; set; }
    
    [JsonProperty("last")]
    public DateTime Last { get; set; }
}