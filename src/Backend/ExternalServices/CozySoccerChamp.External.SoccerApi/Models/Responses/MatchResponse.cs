using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class MatchResponse
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("utcDate")]
    public DateTime StartDateUtc { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("lastUpdated")]
    public DateTime LastUpdatedUtc { get; set; }
    
    [JsonProperty("homeTeam")]
    public TeamResponse HomeTeam { get; set; }
    
    [JsonProperty("awayTeam")]
    public TeamResponse AwayTeam { get; set; }
    
    [JsonProperty("score")]
    public MatchResultResponse MatchResult { get; set; }
    
    [JsonProperty("matchday")]
    public int? Matchday { get; set; }
    
    [JsonProperty("stage")]
    public string Stage { get; set; }
    
    [JsonProperty("group")]
    public string Group { get; set; }
}