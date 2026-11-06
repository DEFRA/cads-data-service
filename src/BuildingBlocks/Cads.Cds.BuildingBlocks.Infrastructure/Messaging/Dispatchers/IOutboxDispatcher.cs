using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;

public interface IOutboxDispatcher<in TClient>
    where TClient : IQueueClient
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
}