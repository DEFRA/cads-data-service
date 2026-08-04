using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.StorageBridge.Application.Messaging.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Consumers;

public class StorageBridgeFifoQueueListener(
    IQueuePoller<StorageBridgeFifoQueueClient> queuePoller,
    ILogger<StorageBridgeFifoQueueListener> logger)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("StorageBridgeFifoQueueListener start requested.");

        return queuePoller.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("StorageBridgeFifoQueueListener stop requested.");

        try
        {
            await queuePoller.StopAsync(cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // Swallow expected cancellation
        }
        catch (ObjectDisposedException)
        {
            // Swallow: poller was already disposed (e.g. by the DI container) before
            // the hosted-service stop sequence completed.
        }
    }
}