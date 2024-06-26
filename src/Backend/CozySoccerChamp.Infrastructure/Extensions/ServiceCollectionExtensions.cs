using CozySoccerChamp.External.SoccerApi.Extensions;
using Quartz;

namespace CozySoccerChamp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddAutoMapper(typeof(MappingProfile));

        services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddBackgroundServices(configuration)
            .AddSoccerApiClient(configuration);

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        const string sectionName = "DefaultDbConnection";

        var connectionString = configuration.GetConnectionString(sectionName)
                               ?? throw new InvalidOperationException("ConnectionString not found");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IMatchResultRepository, MatchResultRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPredictionRepository, PredictionRepository>();

        return services;
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTelegramWebhook(configuration)
            .AddQuartzJobs(configuration);

        return services;
    }

    private static IServiceCollection AddTelegramWebhook(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(BotSettings.SectionName).Get<BotSettings>()
                       ?? throw new InvalidOperationException($"{BotSettings.SectionName} not found");

        services.AddSingleton(settings);

        services.AddHostedService<TelegramWebhookHostedService>();

        services
            .AddHttpClient(settings.ClientName)
            .AddTypedClient<ITelegramBotClient>((httpClient, _) =>
            {
                var options = new TelegramBotClientOptions(settings.BotToken);

                return new TelegramBotClient(options, httpClient);
            });


        return services;
    }

    private static IServiceCollection AddQuartzJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(quartz =>
        {
            // quartz
            //     .AddJobAndTrigger<DataProcessingJob>(configuration);
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }

    private static IServiceCollectionQuartzConfigurator AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration configuration)
        where T : IJob
    {
        const string sectionName = "JobScheduleSettings";

        var jobName = typeof(T).Name;
        var configKey = $"{sectionName}:{jobName}";
        var cronSchedule = configuration[configKey];

        if (string.IsNullOrEmpty(cronSchedule))
            throw new InvalidOperationException($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");

        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(options => options.WithIdentity(jobKey));

        quartz.AddTrigger(options => options
            .WithIdentity(jobName + "-trigger")
            .ForJob(jobKey)
            .WithCronSchedule(cronSchedule));

        return quartz;
    }
}