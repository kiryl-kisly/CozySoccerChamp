using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class ResultSetResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("first")]
    public DateTime First { get; set; }
    
    [JsonPropertyName("last")]
    public DateTime Last { get; set; }
}