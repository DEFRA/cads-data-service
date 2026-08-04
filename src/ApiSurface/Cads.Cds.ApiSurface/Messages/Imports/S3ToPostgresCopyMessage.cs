namespace Cads.Cds.ApiSurface.Messages.Imports;

public class S3ToPostgresCopyMessage : MessageType
{
    public long FileImportId { get; init; }
    public string ObjectKey { get; init; } = string.Empty;
    public string CorrelationId { get; init; } = string.Empty;
}