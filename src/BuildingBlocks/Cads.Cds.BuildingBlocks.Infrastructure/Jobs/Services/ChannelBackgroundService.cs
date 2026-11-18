using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Jobs.Services;

public abstract class ChannelBackgroundService<T>(
    Channel<T> channel,
    ILogger logger,
    int maxParallelJobs = 5) : BackgroundService
    where T : IQueueJob
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var semaphore = new SemaphoreSlim(maxParallelJobs);
        var tasks = new ConcurrentBag<Task>();

        try
        {
            await foreach (var job in channel.Reader.ReadAllAsync(stoppingToken))
            {
                await semaphore.WaitAsync(stoppingToken);
                tasks.Add(ProcessJobAsync(job, semaphore, stoppingToken));
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Graceful shutdown requested - stop accepting new jobs but still allow
            // in-flight jobs to complete below.
        }
        finally
        {
            // Await in-flight jobs so their failure/interruption handling completes
            // within the host shutdown timeout window.
            await Task.WhenAll(tasks);
        }
    }

    protected virtual async Task ProcessJobAsync(
        T job,
        SemaphoreSlim semaphore,
        CancellationToken cancellationToken)
    {
        using (CorrelationScope.Begin(job.CorrelationId))
        {
            try
            {
                await HandleAsync(job, cancellationToken);
            }
            catch (Exception ex)
            {
                if (logger.IsEnabled(LogLevel.Error))
                {
                    logger.LogError(ex, "Failed to process job {JobId}", job.JobId);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }

    protected abstract Task HandleAsync(T job, CancellationToken cancellationToken);
}