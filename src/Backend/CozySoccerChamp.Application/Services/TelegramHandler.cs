using CozySoccerChamp.Infrastructure.BackgroundServices.TelegramWebhook;
using Telegram.Bot;

namespace CozySoccerChamp.Application.Services;

public class TelegramHandler(
    ITelegramBotClient client,
    IUserService userService,
    BotSettings settings) : ITelegramHandler
{
    private const string StartPlayButton = "Play in 1 click";
    private const string WelcomeMessage = "Hello! Let`s GO!";

    public async Task HandleUpdateAsync(Update? update)
    {
        if (update?.Message?.Chat == null)
            return;

        try
        {
            await userService.CreateOrGetAsync(update);

            var inlineKeyboardButton = InlineKeyboardButton.WithWebApp(
                text: StartPlayButton,
                webAppInfo: new WebAppInfo { Url = settings.ClientUrl });

            await client.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: WelcomeMessage,
                replyMarkup: new InlineKeyboardMarkup(inlineKeyboardButton));
        }
        catch
        {
            // ignored
        }
    }
}