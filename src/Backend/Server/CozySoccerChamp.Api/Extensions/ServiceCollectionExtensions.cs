using System.Reflection;
using CozySoccerChamp.Api.Exceptions;
using CozySoccerChamp.Infrastructure.Filters;

namespace CozySoccerChamp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        services
            .AddProblemDetails()
            .AddExceptionHandler<ExceptionHandler>()
            .AddControllers(options =>
            {
                options.Filters.Add<RequestResponseLoggingFilter>();
                options.Filters.Add<ValidationTelegramRequestFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

        return services;
    }
}