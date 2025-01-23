using System.Text.Json.Serialization;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class BaseSoccerResponse
{
    [JsonPropertyName("competition")]
    public CompetitionResponse Competition { get; set; }
    
    [JsonPropertyName("resultSet")]
    public ResultSetResponse ResultSet { get; set; }
    
    [JsonPropertyName("matches")]
    public IReadOnlyCollection<MatchResponse> Matches { get; set; }

    [JsonPropertyName("teams")]
    public IReadOnlyCollection<TeamResponse> Teams { get; set; }
}