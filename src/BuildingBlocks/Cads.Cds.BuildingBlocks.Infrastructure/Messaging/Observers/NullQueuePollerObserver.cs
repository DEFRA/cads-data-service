using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Observers;

[ExcludeFromCodeCoverage]
public class NullQueuePollerObserver<T> : IQueuePollerObserver<T>
{
    public void OnMessageHandled(string messageId, DateTime handledAt, T? payload, Message rawMessage) { }
    public void OnMessageFailed(string messageId, DateTime failedAt, Exception exception, Message rawMessage) { }
}