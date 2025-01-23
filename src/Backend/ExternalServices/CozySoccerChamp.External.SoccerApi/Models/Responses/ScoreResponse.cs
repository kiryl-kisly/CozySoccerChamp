using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class ScoreResponse
{
    [JsonPropertyName("home")]
    public int? HomeTeamScore { get; set; }
    
    [JsonPropertyName("away")]
    public int? AwayTeamScore { get; set; }
}