using System.Reflection;
using CozySoccerChamp.Api.Exceptions;
using CozySoccerChamp.Infrastructure.Filters;
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
                                    | HttpLoggingFields.ResponseBody
                                    | HttpLoggingFields.Duration;
        });

        services
            .AddProblemDetails()
            .AddExceptionHandler<ExceptionHandler>()
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

        return services;
    }
}