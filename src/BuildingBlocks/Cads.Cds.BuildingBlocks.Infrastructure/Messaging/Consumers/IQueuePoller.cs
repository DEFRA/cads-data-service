using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;

public interface IQueuePoller<in T>
    where T : IQueueClient
{
    string? QueueUrl { get; }

    Task StartAsync(CancellationToken token);
    Task StopAsync(CancellationToken token);
}