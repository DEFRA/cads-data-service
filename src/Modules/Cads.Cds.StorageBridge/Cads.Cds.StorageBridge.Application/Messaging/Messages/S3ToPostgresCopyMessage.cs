using Cads.Cds.BuildingBlocks.Application.Messaging.Messages;

namespace Cads.Cds.StorageBridge.Application.Messaging.Messages;

public class S3ToPostgresCopyMessage : MessageType
{
    public string ObjectKey { get; init; } = string.Empty;
    public DateTime TransferredAtUtc { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
}
