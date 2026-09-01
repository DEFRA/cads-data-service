namespace Cads.Cds.ApiSurface.Dtos.Imports;

public class FileImportDto
{
    public long Id { get; set; }

    public required string DestinationTableName { get; set; }

    public required string FileName { get; set; }

    public string? GroupKey { get; set; }

    public string? LastFilePartImported { get; set; }

    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }
    public long? RowsImported { get; set; }
    public FileImportStatus ImportStatus { get; set; }

    public FileProcessingStatus ProcessingStatus { get; set; }

    public DateTimeOffset AddedAt { get; set; }

    public DateTimeOffset? ImportStartAt { get; set; }

    public DateTimeOffset? ImportEndAt { get; set; }

    public DateTimeOffset? ProcessingStartAt { get; set; }

    public DateTimeOffset? ProcessingEndAt { get; set; }

    public int FailedAttempts { get; set; }

    public string? LastErrorReason { get; set; }
}