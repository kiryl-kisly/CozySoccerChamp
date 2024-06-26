using Microsoft.Extensions.Hosting;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.TelegramWebhook;

public class TelegramWebhookHostedService(IServiceProvider serviceProvider, BotSettings botSettings) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var webhookAddress = $"{botSettings.HostAddress}{botSettings.Route}";

        var allowedUpdates = new[]
        {
            UpdateType.Message,
        };

        await botClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: allowedUpdates,
            secretToken: botSettings.SecretToken,
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}