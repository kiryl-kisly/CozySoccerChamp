using CozySoccerChamp.Infrastructure.Constants;
using CozySoccerChamp.Infrastructure.Extensions;
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
            context.Result = new ObjectResult($"\"{HeaderParams.TelegramUserId}\" not found")
            {
                StatusCode = 401
            };

            return;
        }

        if (TryGetTelegramUserId(telegramUserIdValue, out var telegramUserId))
        {
            var applicationUser = await userRepository.FindAsync(x => x.TelegramUserId == telegramUserId);

            if (applicationUser is null)
            {
                var user = new ApplicationUser { TelegramUserId = telegramUserId };

                await userRepository.AddAsync(user);
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