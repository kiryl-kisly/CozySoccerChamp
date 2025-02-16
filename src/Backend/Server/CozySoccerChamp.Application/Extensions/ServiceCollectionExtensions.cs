using CozySoccerChamp.Application.DataProviders;
using CozySoccerChamp.Application.DataProviders.Abstractions;
using CozySoccerChamp.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CozySoccerChamp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services
            .AddServices()
            .AddDataProviders();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITelegramHandler, TelegramHandler>();

        services.AddScoped<IInitDataService, InitDataService>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INotificationService, NotificationService>();

        services.AddScoped<ICompetitionService, CompetitionService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IPredictionService, PredictionService>();
        services.AddScoped<ILeaderboardService, LeaderboardService>();

        return services;
    }

    private static IServiceCollection AddDataProviders(this IServiceCollection services)
    {
        services.AddScoped<IResponseDataProvider, CompetitionDataProvider>();
        services.AddScoped<IResponseDataProvider, UserProfileDataProvider>();
        services.AddScoped<IResponseDataProvider, MatchDataProvider>();
        services.AddScoped<IResponseDataProvider, PredictionDataProvider>();
        services.AddScoped<IResponseDataProvider, LeaderboardDataProvider>();
        
        services.AddScoped<IUserProfileDataProvider, LeaderboardDataProvider>();
        
        return services;
    }
}