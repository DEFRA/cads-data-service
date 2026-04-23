using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Application.Services;

public class BulkImportBackgroundService(
    Channel<CreateBulkImportJobDto> channel,
    ILogger<BulkImportBackgroundService> logger,
    IBulkImportCopyService bulkImportCopyService) : BackgroundService
{
    private readonly Channel<CreateBulkImportJobDto> _channel = channel;
    private readonly ILogger<BulkImportBackgroundService> _logger = logger;
    private readonly int _maxParallelImports = 5;
 
    protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var semaphore = new SemaphoreSlim(_maxParallelImports);
        var runningTasks = new ConcurrentBag<Task>();

        await foreach (var request in _channel.Reader.ReadAllAsync(cancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Cancellation requested, aborting split");
                return;
            }

            await semaphore.WaitAsync(cancellationToken);

            var task = Task.Run(async () =>
            {
                try
                {
                   var result = await bulkImportCopyService.ExecuteAsync(request, cancellationToken);

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
                    _logger.LogError(ex, "Failed to copy file {Key}", request.SourceKey);
                }
                finally
                {
                    semaphore.Release();
                }
            }, cancellationToken);

            runningTasks.Add(task);
        }

        await Task.WhenAll(runningTasks);
    }
}