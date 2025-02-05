using System.Diagnostics;
using Quartz;

namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs;

public abstract class BaseJob(ILogger<BaseJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            logger.LogInformation("---> Job is started...");
            
            await ExecuteAsync(context);
            
            stopwatch.Stop();
            logger.LogInformation($"<--- Job is ended. Execution time: {stopwatch.Elapsed:mm':'ss':'fff}");
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logger.LogError($"<--- Job is ended. Execution time: {stopwatch.Elapsed:mm':'ss':'fff}.\nException: {ex}");
        }
    }

    protected abstract Task ExecuteAsync(IJobExecutionContext context); // Абстрактный метод для конкретных джоб
}