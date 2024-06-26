namespace CozySoccerChamp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddSwaggerGen()
            .AddRouting(options => options.LowercaseUrls = true)
            .AddControllers()
            .AddNewtonsoftJson();

        return services;
    }
}