using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestDoubles.Publishers;

/// <summary>
/// Test double for IMessagePublisher{TClient} that records publish attempts (and the resulting
/// message/metadata) so component tests can await a publish without sleeping/polling, and can
/// optionally force the next publish attempt to throw (to exercise dispatcher failure handling).
/// </summary>
public class TestMessagePublisher<TClient> : IMessagePublisher<TClient>
    where TClient : IQueueClient, new()
{
    private TaskCompletionSource<(object Message, FifoMessageMetadata Metadata)> _publishedTcs = CreateTcs();
    private TaskCompletionSource<(object Message, FifoMessageMetadata Metadata)> _attemptedTcs = CreateTcs();

    private Exception? _throwOnNextPublish;

    public string? QueueUrl => "https://sqs.test.local/queue/test-queue.fifo";

    /// <summary>Completes once a message is successfully published.</summary>
    public Task<(object Message, FifoMessageMetadata Metadata)> Published => _publishedTcs.Task;

    /// <summary>Completes on every publish attempt, whether it succeeds or is made to throw.</summary>
    public Task<(object Message, FifoMessageMetadata Metadata)> PublishAttempted => _attemptedTcs.Task;

    public void ThrowOnNextPublish(Exception exception) => _throwOnNextPublish = exception;

    public void Reset()
    {
        _publishedTcs = CreateTcs();
        _attemptedTcs = CreateTcs();
        _throwOnNextPublish = null;
    }

    public Task PublishAsync<TMessage>(TMessage? message, FifoMessageMetadata metadata, CancellationToken cancellationToken = default)
        where TMessage : class
    {
        _attemptedTcs.TrySetResult((message!, metadata));

        if (_throwOnNextPublish is { } exception)
        {
            _throwOnNextPublish = null;
            throw exception;
        }

        _publishedTcs.TrySetResult((message!, metadata));

        return Task.CompletedTask;
    }

    private static TaskCompletionSource<(object Message, FifoMessageMetadata Metadata)> CreateTcs() =>
        new(TaskCreationOptions.RunContinuationsAsynchronously);
}

