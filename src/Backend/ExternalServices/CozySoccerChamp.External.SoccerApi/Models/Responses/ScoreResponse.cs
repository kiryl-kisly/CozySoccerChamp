using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class ScoreResponse
{
    [JsonProperty("home")]
    public int? HomeTeamScore { get; set; }
    
    [JsonProperty("away")]
    public int? AwayTeamScore { get; set; }
}