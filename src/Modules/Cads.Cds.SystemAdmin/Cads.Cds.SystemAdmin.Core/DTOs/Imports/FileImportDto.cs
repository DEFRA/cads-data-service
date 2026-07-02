using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Core.DTOs.Imports;

public class FileImportDto(
    long id,
    string destinationTableName,
    string fileName,
    long totalRowsToProcess,
    long rowsFound,
    FileImportStatus importStatus,
    FileProcessingStatus processingStatus,
    DateTimeOffset addedAt,
    DateTimeOffset? importStartAt,
    DateTimeOffset? importEndAt,
    DateTimeOffset? processingStartAt,
    DateTimeOffset? processingEndAt)
{
    public long Id { get; set; } = id;

    public string DestinationTableName { get; set; } = destinationTableName;
    public string FileName { get; set; } = fileName;

    public long TotalRowsToProcess { get; set; } = totalRowsToProcess;
    public long RowsFound { get; set; } = rowsFound;

    public FileImportStatus ImportStatus { get; set; } = importStatus;
    public FileProcessingStatus ProcessingStatus { get; set; } = processingStatus;

    public DateTimeOffset AddedAt { get; set; } = addedAt;
    public DateTimeOffset? ImportStartAt { get; set; } = importStartAt;
    public DateTimeOffset? ImportEndAt { get; set; } = importEndAt;
    public DateTimeOffset? ProcessingStartAt { get; set; } = processingStartAt;
    public DateTimeOffset? ProcessingEndAt { get; set; } = processingEndAt;
}