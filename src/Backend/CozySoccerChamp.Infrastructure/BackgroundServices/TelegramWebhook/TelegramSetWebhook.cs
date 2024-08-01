using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.TelegramWebhook;

public class TelegramSetWebhook(
    IServiceProvider serviceProvider,
    BotSettings botSettings,
    ILogger<TelegramSetWebhook> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
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

        logger.LogInformation("Telegram set webhook: {Url}", webhookAddress);
    }
}