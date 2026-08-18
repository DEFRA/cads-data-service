using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Polly;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Publishers;

public class RetryingMessagePublisher<TClient>(
    IMessagePublisher<TClient> inner,
    ResiliencePipeline pipeline)
    : IMessagePublisher<TClient> where TClient : IQueueClient, new()
{
    public string QueueUrl => inner.QueueUrl!;

    public Task PublishAsync<TMessage>(TMessage? message, FifoMessageMetadata metadata, CancellationToken cancellationToken = default)
        where TMessage : class
        => pipeline.ExecuteAsync(ct => new ValueTask<Task>(inner.PublishAsync(message, metadata, ct)),
            cancellationToken).AsTask().Unwrap();
}