using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Core.Extensions;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports;

public class FileImport
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
    public int FailedAttempts { get; set; }
    public string? LastErrorReason { get; set; }

    public FileImport() { }

    private FileImport(
        string destinationTableName,
        string fileName,
        long totalRowsToProcess,
        long rowsFound)
    {
        DestinationTableName = destinationTableName;
        FileName = StringExtensions.NormalizeToUpper(fileName)!;

        TotalRowsToProcess = totalRowsToProcess;
        RowsFound = rowsFound;

        ImportStatus = FileImportStatus.Pending;
        ProcessingStatus = FileProcessingStatus.Pending;

        AddedAt = DateTimeOffset.UtcNow;

        FailedAttempts = 0;
    }

    public static FileImport Create(
        string destinationTableName,
        string fileName,
        long totalRowsToProcess,
        long rowsFound)
        => new(
            destinationTableName,
            fileName,
            totalRowsToProcess,
            rowsFound);

    public void SetTotalRowsToProcess(long total)
    {
        if (TotalRowsToProcess == total) return;

        TotalRowsToProcess = total;
    }

    public void SetRowsFound(long total)
    {
        if (RowsFound == total) return;

        RowsFound = total;
    }

    public void SetImportStatus(FileImportStatus status)
    {
        if (ImportStatus == status) return;

        if (status == FileImportStatus.Failed)
        {
            MarkFailed("Import failed.");
            return;
        }

        (status switch
        {
            FileImportStatus.Transferred => (Action)MarkTransferred,
            FileImportStatus.Split => MarkSplit,
            FileImportStatus.Completed => MarkCompleted,
            _ => null
        })?.Invoke();
    }

    // -----------------------------
    // Importing workflow
    // -----------------------------

    public void MarkTransferred()
    {
        if (ImportStatus != FileImportStatus.Pending)
            throw new DomainException("Transferred can only start from pending.");

        ImportStatus = FileImportStatus.Transferred;
        ImportStartAt = DateTimeOffset.UtcNow;
    }

    public void MarkSplit()
    {
        if (ImportStatus != FileImportStatus.Transferred)
            throw new DomainException("Split can only start from transferred.");

        ImportStatus = FileImportStatus.Split;
    }

    public void MarkCompleted()
    {
        if (ImportStatus != FileImportStatus.Split)
            throw new DomainException("Import must be in split state to complete.");

        ImportStatus = FileImportStatus.Completed;
        ImportEndAt = DateTimeOffset.UtcNow;
    }

    public void MarkFailed(string reason)
    {
        if (ImportStatus == FileImportStatus.Completed)
            throw new DomainException("Import must be in pending, transferred, or split state to be marked as failed.");

        ImportStatus = FileImportStatus.Failed;
        ImportEndAt = DateTimeOffset.UtcNow;

        LastErrorReason = reason;
        FailedAttempts++;
    }

    // -----------------------------
    // Processing workflow
    // -----------------------------

    public void MarkProcessingStarted()
    {
        if (ProcessingStatus != FileProcessingStatus.Pending)
            throw new DomainException("Processing can only start from pending.");

        ProcessingStatus = FileProcessingStatus.Processing;
        ProcessingStartAt = DateTimeOffset.UtcNow;
    }

    public void MarkProcessingComplete()
    {
        if (ProcessingStatus != FileProcessingStatus.Processing)
            throw new DomainException("Processing must be running to complete.");

        ProcessingStatus = FileProcessingStatus.Complete;
        ProcessingEndAt = DateTimeOffset.UtcNow;
    }

    public void MarkProcessingFailed()
    {
        if (ProcessingStatus != FileProcessingStatus.Processing)
            throw new DomainException("Processing must be in processing state to be marked as failed.");

        ProcessingStatus = FileProcessingStatus.Failed;
        ProcessingEndAt = DateTimeOffset.UtcNow;
    }

    // -----------------------------
    // Replay workflow
    // -----------------------------

    public void ResetForReplay()
    {
        ImportStatus = FileImportStatus.Pending;
        ProcessingStatus = FileProcessingStatus.Pending;

        RowsFound = 0;

        ImportStartAt = null;
        ImportEndAt = null;
        ProcessingStartAt = null;
        ProcessingEndAt = null;

        FailedAttempts = 0;
        LastErrorReason = string.Empty;
    }
}