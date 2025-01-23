using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class MatchResultResponse
{
    [JsonPropertyName("winner")]
    public string Winner { get; set; }
    
    [JsonPropertyName("duration")]
    public string Duration { get; set; }
    
    [JsonPropertyName("fullTime")]
    public ScoreResponse FullTime { get; set; }
    
    [JsonPropertyName("halfTime")]
    public ScoreResponse HalfTime { get; set; }
    
    [JsonPropertyName("regularTime")]
    public ScoreResponse RegularTime { get; set; }
    
    [JsonPropertyName("extraTime")]
    public ScoreResponse ExtraTime { get; set; }
    
    [JsonPropertyName("penalties")]
    public ScoreResponse Penalties { get; set; }
}