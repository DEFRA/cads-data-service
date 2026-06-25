using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Core.DTOs.Imports;

public class FileImportDto
{
    public long Id { get; set; }

    public string DestinationTableName { get; set; } = default!;
    public string FileName { get; set; } = default!;

    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }

    public FileImportStatus ImportStatus { get; set; }
    public FileProcessingStatus ProcessingStatus { get; set; }

    public DateTimeOffset AddedAt { get; set; }
    public DateTimeOffset? ImportStartAt { get; set; }
    public DateTimeOffset? ImportEndAt { get; set; }
    public DateTimeOffset? ProcessingStartAt { get; set; }
    public DateTimeOffset? ProcessingEndAt { get; set; }
}
