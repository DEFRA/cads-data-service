using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;

namespace Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;

public interface IMessagePublisher<in T>
    where T : IQueueClient, new()
{
    string? QueueUrl { get; }

    Task PublishAsync<TMessage>(TMessage? message, FifoMessageMetadata metadata, CancellationToken cancellationToken = default) where TMessage : class;
}