namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Types;

public sealed class FileImportBatchPart
{
    public int PartNo { get; set; }
    public string FileName { get; set; } = null!;
    public short ImportStatusId { get; set; }
    public short ProcessingStatusId { get; set; }
    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }
    public long RowsImported { get; set; }
    public short? FailedAttempts { get; set; }
    public string? LastErrorReason { get; set; }
    public string? LastFilePartImported { get; set; }
}
