namespace Cads.Cds.ApiSurface.Messages.Imports;

public class S3ToPostgresCopyMessage : MessageType
{
    public string ObjectKey { get; init; } = string.Empty;
    public DateTime TransferredAtUtc { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
}