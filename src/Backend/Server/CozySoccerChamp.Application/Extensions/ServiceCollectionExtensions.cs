using CozySoccerChamp.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CozySoccerChamp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddInternalServices();

        return services;
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        services.AddScoped<ITelegramHandler, TelegramHandler>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ICompetitionService, CompetitionService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IPredictionService, PredictionService>();
        services.AddScoped<ILeaderboardService, LeaderboardService>();

        return services;
    }
}