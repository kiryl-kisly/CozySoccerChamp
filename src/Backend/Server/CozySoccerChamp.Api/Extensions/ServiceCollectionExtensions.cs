using System.Reflection;
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

        services
            .AddProblemDetails()
            .AddExceptionHandler<ExceptionHandler>()
            .ConfigureTelegramBotMvc()
            .AddControllers();

        return services;
    }
}