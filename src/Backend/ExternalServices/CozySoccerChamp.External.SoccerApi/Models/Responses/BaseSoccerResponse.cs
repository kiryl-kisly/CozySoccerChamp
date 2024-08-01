using Newtonsoft.Json;

namespace CozySoccerChamp.External.SoccerApi.Models.Responses;

public class BaseSoccerResponse
{
    [JsonProperty("matches")]
    public IReadOnlyCollection<MatchResponse> Matches { get; set; }

    [JsonProperty("teams")]
    public IReadOnlyCollection<TeamResponse> Teams { get; set; }
}