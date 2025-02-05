using CozySoccerChamp.Infrastructure.Constants;
using CozySoccerChamp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace CozySoccerChamp.Infrastructure.Filters;

public class AuthenticationTelegramRequestFilter(IApplicationUserRepository userRepository) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var isValid = context.HttpContext.Request.Headers.TryGetValue(HeaderParams.TelegramUserId, out var telegramUserIdValue);

        if (!isValid)
        {
            context.Result = new ObjectResult($"Header {HeaderParams.TelegramUserId} not found")
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            return;
        }

        if (TryGetTelegramUserId(telegramUserIdValue, out var telegramUserId))
        {
            var applicationUser = await userRepository.FindAsync(x => x.TelegramUserId == telegramUserId);

            if (applicationUser is null)
            {
                context.Result = new ObjectResult($"TelegramUserId: {telegramUserId} not found")
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                
                return;
            }
        }
        
        context.HttpContext.SetTelegramUserId(telegramUserId);

        await next();
    }

    private static bool TryGetTelegramUserId(StringValues headerValue, out long telegramUserId)
    {
        return long.TryParse(headerValue, out telegramUserId);
    }
}