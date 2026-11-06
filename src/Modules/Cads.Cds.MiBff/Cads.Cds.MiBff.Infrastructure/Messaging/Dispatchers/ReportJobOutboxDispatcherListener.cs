using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;

/// <summary>
/// Thin IHostedService that starts/stops the actual MiReportJobOutboxDispatcher. Registered
/// separately from IOutboxDispatcher{MiBffFifoQueueClient} so component tests - where
/// WebAppFactoryBase removes all IHostedService registrations - can still resolve and manually
/// drive the dispatcher directly, mirroring StorageBridgeFifoQueueListener/Poller.
/// </summary>
public class ReportJobOutboxDispatcherListener(
    IOutboxDispatcher<MiBffFifoQueueClient> dispatcher,
    ILogger<ReportJobOutboxDispatcherListener> logger)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("ReportJobOutboxDispatcherListener start requested.");

        return dispatcher.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("ReportJobOutboxDispatcherListener stop requested.");

        try
        {
            await dispatcher.StopAsync(cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // Swallow expected cancellation
        }
    }
}

