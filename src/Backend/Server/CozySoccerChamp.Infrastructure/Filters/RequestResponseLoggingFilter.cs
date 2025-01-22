using Microsoft.AspNetCore.Mvc.Filters;

namespace CozySoccerChamp.Infrastructure.Filters;

public abstract class RequestResponseLoggingFilter(ILogger<RequestResponseLoggingFilter> logger) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var request = context.HttpContext.Request;

        logger.LogInformation("Request: {Method} {Path} {QueryString}",
            request.Method,
            request.Path,
            request.QueryString.ToString());
        
        var resultContext = await next();
        
        var response = resultContext.HttpContext.Response;
        logger.LogInformation("Response: {StatusCode}", response.StatusCode);
    }
}