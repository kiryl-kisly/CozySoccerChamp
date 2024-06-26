using CozySoccerChamp.External.SoccerApi.Abstractions;
using CozySoccerChamp.External.SoccerApi.Models.Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CozySoccerChamp.External.SoccerApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSoccerApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(SoccerApiSettings.SectionName).Get<SoccerApiSettings>()
                       ?? throw new InvalidOperationException($"{SoccerApiSettings.SectionName} not found");

        services.AddSingleton(settings);

        services.AddScoped<ISoccerApiClient, SoccerApiClient>();
        
        return services;
    }
}