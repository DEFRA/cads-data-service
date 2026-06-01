using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;

public abstract class S3BulkLoadBackgroundService<T>(
    Channel<T> channel,
    ILogger<S3BulkLoadBackgroundService<T>> logger,
    IS3ToPostgresService<T> processor, IServiceScopeFactory serviceScopeFactory
    ) : BackgroundService
    where T : CreateS3BulkLoadJobDto
{
    private readonly int _maxParallelImports = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var semaphore = new SemaphoreSlim(_maxParallelImports);
        var tasks = new ConcurrentBag<Task>();

        await foreach (var request in channel.Reader.ReadAllAsync(stoppingToken))
        {
            await semaphore.WaitAsync(stoppingToken);

            tasks.Add(ProcessJobAsync(request, semaphore, stoppingToken));
        }

        await Task.WhenAll(tasks);
    }

    private async Task ProcessJobAsync(
        T request,
        SemaphoreSlim semaphore,
        CancellationToken cancellationToken)
    {
        try
        {
            await processor.ExecuteAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process bulk load job {JobId}", request.JobId);
        }
        finally
        {
            semaphore.Release();
        }
    }
}