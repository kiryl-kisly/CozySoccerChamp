using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class MatchResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("utcDate")]
    public DateTime StartDateUtc { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdatedUtc { get; set; }
    
    [JsonPropertyName("homeTeam")]
    public TeamResponse HomeTeam { get; set; }
    
    [JsonPropertyName("awayTeam")]
    public TeamResponse AwayTeam { get; set; }
    
    [JsonPropertyName("score")]
    public MatchResultResponse MatchResult { get; set; }
    
    [JsonPropertyName("matchday")]
    public int? Matchday { get; set; }
    
    [JsonPropertyName("stage")]
    public string Stage { get; set; }
    
    [JsonPropertyName("group")]
    public string Group { get; set; }
    
    [JsonPropertyName("competition")]
    public CompetitionResponse Competition { get; set; }
}