using CozySoccerChamp.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;

namespace CozySoccerChamp.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static void SetTelegramUserId(this HttpContext context, long telegramUserId)
    {
        context.Items[HeaderParams.TelegramUserId] = telegramUserId;
    }

    public static long GetTelegramUserId(this HttpContext context)
    {
        if (!context.Items.TryGetValue(HeaderParams.TelegramUserId, out var value) || value is not long telegramUserId)
        {
            throw new InvalidOperationException($"\"{HeaderParams.TelegramUserId}\" key not set in HttpContext.Items array");
        }

        return telegramUserId;
    }
}