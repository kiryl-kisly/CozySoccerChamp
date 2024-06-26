using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CozySoccerChamp.Infrastructure.Filters;

/// <summary>
/// Check for "X-Telegram-Bot-Api-Secret-Token"
/// Read more: <see href="https://core.telegram.org/bots/api#setwebhook"/> "secret_token"
/// </summary>
public class ValidationTelegramRequestFilter(BotSettings botSettings) : IAsyncActionFilter
{
    private const string TelegramSecretTokenHeader = "X-Telegram-Bot-Api-Secret-Token";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!IsValidRequest(context.HttpContext.Request))
        {
            context.Result = new ObjectResult($"\"{TelegramSecretTokenHeader}\" is invalid")
            {
                StatusCode = 403
            };

            return;
        }

        await next();
    }

    private bool IsValidRequest(HttpRequest request)
    {
        var isSecretTokenProvided = request.Headers.TryGetValue(TelegramSecretTokenHeader, out var secretTokenHeader);

        return isSecretTokenProvided && string.Equals(secretTokenHeader, botSettings.SecretToken, StringComparison.Ordinal);
    }
}