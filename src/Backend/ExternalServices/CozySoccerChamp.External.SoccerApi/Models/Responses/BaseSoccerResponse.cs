using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class BaseSoccerResponse
{
    [JsonProperty("competition")]
    public CompetitionResponse Competition { get; set; }
    
    [JsonProperty("resultSet")]
    public ResultSetResponse ResultSet { get; set; }
    
    [JsonProperty("matches")]
    public IReadOnlyCollection<MatchResponse> Matches { get; set; }

    [JsonProperty("teams")]
    public IReadOnlyCollection<TeamResponse> Teams { get; set; }
}