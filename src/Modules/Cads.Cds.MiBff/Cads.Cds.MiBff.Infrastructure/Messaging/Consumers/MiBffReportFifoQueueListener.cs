using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Consumers;

public class MiBffReportFifoQueueListener(
    IQueuePoller<MiBffFifoQueueClient> queuePoller,
    ILogger<MiBffReportFifoQueueListener> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("MiBffReportFifoQueueListener start requested.");

        return queuePoller.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("MiBffReportFifoQueueListener stop requested.");

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