using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Application.Services;

public class BulkImportBackgroundService(
    Channel<CreateBulkImportJobDto> channel,
    IServiceScopeFactory scopeFactory,
    ILogger<BulkImportBackgroundService> logger) : BackgroundService
{
    private readonly int _maxParallelImports = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var semaphore = new SemaphoreSlim(_maxParallelImports);
        var runningTasks = new ConcurrentBag<Task>();
        IBulkImportCopyService? service = null;

        using var scope = scopeFactory.CreateScope();

        await foreach (var request in channel.Reader.ReadAllAsync(stoppingToken))
        {
            service = service ?? scope.ServiceProvider.GetRequiredService<IBulkImportCopyService>();

            if (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Cancellation requested, aborting import");
                return;
            }

            await semaphore.WaitAsync(stoppingToken);

            var task = Task.Run(async () =>
            {
                try
                {
                    var result = await service.ExecuteAsync(request, stoppingToken);

                    if (result)
                    {
                        //_progressStore.MarkSucceeded(request.JobId, request.SourceKey);
                    }
                    else
                    {
                        //_progressStore.MarkFailed(request.JobId, request.SourceKey, "Unknown error during splt");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to copy file {Key}", request.SourceKey);
                }
                finally
                {
                    semaphore.Release();
                }
            }, stoppingToken);

            runningTasks.Add(task);
        }

        await Task.WhenAll(runningTasks);
    }
}