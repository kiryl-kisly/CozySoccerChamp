namespace CozySoccerChamp.Infrastructure.BackgroundServices.TelegramWebhook;

public class BotSettings
{
    public static readonly string SectionName = nameof(BotSettings);

    public string BotToken { get; init; } = default!;
    public string HostAddress { get; init; } = default!;
    public string Route { get; init; } = default!;
    public string SecretToken { get; init; } = default!;
    public string ClientName { get; init; } = default!;
}