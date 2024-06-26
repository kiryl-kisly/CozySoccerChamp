using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class MatchResultResponse
{
    [JsonProperty("home")]
    public int? HomeTeamScore { get; set; }
    
    [JsonProperty("away")]
    public int? AwayTeamScore { get; set; }
}