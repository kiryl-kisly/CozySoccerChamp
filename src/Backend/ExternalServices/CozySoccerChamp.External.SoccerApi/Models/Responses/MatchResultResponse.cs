using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class MatchResultResponse
{
    [JsonProperty("winner")]
    public string Winner { get; set; }
    
    [JsonProperty("duration")]
    public string Duration { get; set; }
    
    [JsonProperty("fullTime")]
    public ScoreResponse FullTime { get; set; }
    
    [JsonProperty("halfTime")]
    public ScoreResponse HalfTime { get; set; }
    
    [JsonProperty("regularTime")]
    public ScoreResponse RegularTime { get; set; }
    
    [JsonProperty("extraTime")]
    public ScoreResponse ExtraTime { get; set; }
    
    [JsonProperty("penalties")]
    public ScoreResponse Penalties { get; set; }
}