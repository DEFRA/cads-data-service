using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public abstract class S3ImportBackgroundService<T>(
    Channel<T> channel,
    ILogger<S3ImportBackgroundService<T>> logger,
    IS3ToPostgresService<T> processor) : BackgroundService
    where T : CreateS3ImportJobDto
{
    private readonly int _maxParallelImports = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var semaphore = new SemaphoreSlim(_maxParallelImports);
        var tasks = new ConcurrentBag<Task>();

        try
        {
            await foreach (var request in channel.Reader.ReadAllAsync(stoppingToken))
            {
                await semaphore.WaitAsync(stoppingToken);
                tasks.Add(ProcessJobAsync(request, semaphore, stoppingToken));
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Graceful shutdown requested - stop accepting new jobs but still allow
            // in-flight jobs (and their status-update cleanup) to complete below.
        }
        finally
        {
            // Await in-flight jobs so their failure/interruption status is persisted
            // within the host shutdown timeout window.
            await Task.WhenAll(tasks);
        }
    }

    protected virtual async Task ProcessJobAsync(
        T request,
        SemaphoreSlim semaphore,
        CancellationToken cancellationToken)
    {
        using (CorrelationScope.Begin(request.CorrelationId))
        {
            try
            {
                await processor.ExecuteAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                if (logger.IsEnabled(LogLevel.Error))
                {
                    logger.LogError(ex, "Failed to process bulk load job {JobId}", request.JobId);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}