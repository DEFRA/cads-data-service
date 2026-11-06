using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;

/// <summary>
/// Resolvable, manually start/stop-able outbox dispatcher contract. Kept separate from
/// IHostedService so component tests can resolve and drive it directly (mirrors IQueuePoller{T}),
/// without relying on the generic host's hosted-service lifecycle.
/// </summary>
public interface IOutboxDispatcher<in TClient>
    where TClient : IQueueClient
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
}

