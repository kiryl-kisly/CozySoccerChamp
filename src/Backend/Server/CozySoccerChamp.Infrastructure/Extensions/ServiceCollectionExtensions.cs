using CozySoccerChamp.External.SoccerApi.Extensions;
using CozySoccerChamp.Infrastructure.BackgroundServices;
using CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;
using CozySoccerChamp.Infrastructure.BackgroundServices.Jobs.Settings;
using CozySoccerChamp.Infrastructure.Mappers;
using Quartz;

namespace CozySoccerChamp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddSoccerApiClient(configuration)
            .AddTelegramClient(configuration)
            .AddBackgroundServices(configuration);

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        const string sectionName = "CozySoccerChampPostgresSqlConnection";

        var connectionString = configuration.GetConnectionString(sectionName)
                               ?? throw new ApplicationException("ConnectionString not found");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<INotificationSettingsRepository, NotificationSettingsRepository>();

        services.AddScoped<ICompetitionRepository, CompetitionRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IMatchResultRepository, MatchResultRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPredictionRepository, PredictionRepository>();

        return services;
    }

    private static IServiceCollection AddTelegramClient(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(BotSettings.SectionName).Get<BotSettings>()
                       ?? throw new ApplicationException($"{BotSettings.SectionName} not found");

        services.AddSingleton(settings);

        services
            .AddHttpClient(settings.ClientName)
            .AddTypedClient<ITelegramBotClient>((httpClient, _) =>
            {
                var options = new TelegramBotClientOptions(settings.BotToken);

                return new TelegramBotClient(options, httpClient);
            });

        return services;
    }

    private static void AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(PointCalculateSettings.SectionName).Get<PointCalculateSettings>()
                       ?? throw new ApplicationException($"{PointCalculateSettings.SectionName} not found");

        services.AddSingleton(settings);

        services
            .AddHostedService<DataInitialization>()
            .AddHostedService<TelegramSetWebhook>();

        services.AddQuartzJobs(configuration);
    }

    private static void AddQuartzJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(quartz =>
        {
            quartz
                .AddJobAndTrigger<MatchDataProcessingJob>(configuration)
                .AddJobAndTrigger<PointCalculatingJob>(configuration)
                .AddJobAndTrigger<AnnouncementNotificationJob>(configuration)
                .AddJobAndTrigger<ReminderNotificationJob>(configuration);
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    private static IServiceCollectionQuartzConfigurator AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration configuration)
        where T : IJob
    {
        const string sectionName = "JobScheduleSettings";

        var jobName = typeof(T).Name;
        var configKey = $"{sectionName}:{jobName}";
        var cronSchedule = configuration[configKey];

        if (string.IsNullOrEmpty(cronSchedule))
            throw new ApplicationException($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");

        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(options => options.WithIdentity(jobKey));

        quartz.AddTrigger(options => options
            .WithIdentity(jobName + "-trigger")
            .ForJob(jobKey)
            .WithCronSchedule(cronSchedule));

        return quartz;
    }
}