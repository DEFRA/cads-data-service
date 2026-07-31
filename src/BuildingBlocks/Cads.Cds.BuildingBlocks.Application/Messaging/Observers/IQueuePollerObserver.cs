using Amazon.SQS.Model;

namespace Cads.Cds.BuildingBlocks.Application.Messaging.Observers;

public interface IQueuePollerObserver<in T>
{
    void OnMessageHandled(string messageId, DateTime handledAt, T? payload, Message rawMessage);
    void OnMessageFailed(string messageId, DateTime failedAt, Exception exception, Message rawMessage);
}
