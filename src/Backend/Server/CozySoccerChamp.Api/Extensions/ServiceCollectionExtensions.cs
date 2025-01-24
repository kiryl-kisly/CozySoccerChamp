using System.Reflection;
using System.Text.Json.Serialization;
using CozySoccerChamp.Api.Exceptions;
using Microsoft.AspNetCore.HttpLogging;

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
            .AddHttpLogging()
            .AddProblemDetails()
            .AddExceptionHandler<ExceptionHandler>()
            .ConfigureTelegramBotMvc()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }

    private static IServiceCollection AddHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(options =>
        {
            options.CombineLogs = true;

            options.LoggingFields = HttpLoggingFields.RequestQuery
                                    | HttpLoggingFields.RequestMethod
                                    | HttpLoggingFields.RequestPath
                                    | HttpLoggingFields.RequestBody
                                    | HttpLoggingFields.ResponseStatusCode
                                    | HttpLoggingFields.Duration;
        });

        return services;
    }
}