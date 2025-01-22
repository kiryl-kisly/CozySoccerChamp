using CozySoccerChamp.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CozySoccerChamp.Infrastructure.Filters;

/// <summary>
/// Check for "X-Telegram-Bot-Api-Secret-Token"
/// Read more: <see href="https://core.telegram.org/bots/api#setwebhook"/> "secret_token"
/// </summary>
public abstract class ValidationTelegramRequestFilter(BotSettings botSettings) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!IsValidRequest(context.HttpContext.Request))
        {
            context.Result = new ObjectResult($"\"{HeaderParams.TelegramSecretToken}\" is invalid")
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            return;
        }

        await next();
    }

    private bool IsValidRequest(HttpRequest request)
    {
        var isSecretTokenProvided = request.Headers.TryGetValue(HeaderParams.TelegramSecretToken, out var secretTokenHeader);

        return isSecretTokenProvided && string.Equals(secretTokenHeader, botSettings.SecretToken, StringComparison.Ordinal);
    }
}