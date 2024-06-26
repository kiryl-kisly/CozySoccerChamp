using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class ScoreResponse
{
    [JsonProperty("winner")]
    public string Winner { get; set; }
    
    [JsonProperty("duration")]
    public string Duration { get; set; }
    
    [JsonProperty("fullTime")]
    public MatchResultResponse FullTime { get; set; }
    
    [JsonProperty("halfTime")]
    public MatchResultResponse HalfTime { get; set; }
}