using Telegram.Bot.Types;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.TelegramWebhook;

public class TelegramSetWebhook(
    IServiceProvider serviceProvider,
    BotSettings botSettings,
    ILogger<TelegramSetWebhook> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var webhookAddress = $"{botSettings.HostAddress}{botSettings.Route}";

        var allowedUpdates = new[]
        {
            UpdateType.Message
        };

        await botClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: allowedUpdates,
            secretToken: botSettings.SecretToken,
            cancellationToken: cancellationToken);

        await SetupBaseCommandAsync(botClient);

        logger.LogInformation("Telegram set webhook: {Url}", webhookAddress);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }

    private static async Task SetupBaseCommandAsync(ITelegramBotClient client)
    {
        var commands = new[]
        {
            new BotCommand { Command = "start", Description = "Start" }
        };

        await client.SetMyCommandsAsync(commands);
    }
}