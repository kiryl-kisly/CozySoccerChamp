using CozySoccerChamp.External.SoccerApi.Abstractions;
using CozySoccerChamp.External.SoccerApi.Models.Infrastructures;
using CozySoccerChamp.External.SoccerApi.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace CozySoccerChamp.External.SoccerApi;

public class SoccerApiClient(SoccerApiSettings settings) : ISoccerApiClient
{
    private const string XAuthTokenHeader = "X-Auth-Token";

    private const string MatchesRoute = $"/v4/competitions/EC/matches";
    private const string TeamsRoute = "/v4/competitions/EC/teams";
    private const string SeasonQueryParameterName = "season";
    
    private readonly RestClientOptions _restClientOptions = new(settings.BaseUrl)
    {
        ThrowOnAnyError = true,
        MaxTimeout = 5000 // 5 second 
    };
    
    public async Task<IReadOnlyCollection<MatchResponse>> GetMatchesAsync(int season)
    {
        try
        {
            var request = new RestRequest(MatchesRoute);

            request.AddQueryParameter(SeasonQueryParameterName, season);
            request.AddHeader(XAuthTokenHeader, settings.ApiToken);

            var client = new RestClient(_restClientOptions);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Response status code is {response.StatusCode}.\nContent: {response.Content}");

            var soccerApiResponse = response.Content is null
                ? null
                : JsonConvert.DeserializeObject<BaseSoccerResponse>(response.Content);

            if (soccerApiResponse is not null)
                return soccerApiResponse.Matches;
        }
        catch
        {
            // ignored
        }

        return null;
    }

    public async Task<IReadOnlyCollection<TeamResponse>> GetTeamsAsync(int season)
    {
        try
        {
            var request = new RestRequest(TeamsRoute);

            request.AddQueryParameter(SeasonQueryParameterName, season);
            request.AddHeader(XAuthTokenHeader, settings.ApiToken);

            var client = new RestClient(_restClientOptions);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Response status code is {response.StatusCode}.\nContent: {response.Content}");

            var soccerApiResponse = response.Content is null
                ? null
                : JsonConvert.DeserializeObject<BaseSoccerResponse>(response.Content);

            if (soccerApiResponse is not null)
                return soccerApiResponse.Teams;
        }
        catch
        {
            // ignored
        }

        return null;
    }
}