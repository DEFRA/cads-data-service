using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports.BusinessRules;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Core.Extensions;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports;

public class FileImport
{
    public long Id { get; set; }

    public string DestinationTableName { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? LastFilePartImported { get; set; }

    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }
    public long RowsImported { get; set; }

    public FileImportStatus ImportStatus { get; set; } = FileImportStatus.Pending;
    public FileProcessingStatus ProcessingStatus { get; set; } = FileProcessingStatus.Pending;
    public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ImportStartAt { get; set; }
    public DateTimeOffset? ImportEndAt { get; set; }
    public DateTimeOffset? ProcessingStartAt { get; set; }
    public DateTimeOffset? ProcessingEndAt { get; set; }
    public int FailedAttempts { get; set; }
    public string? LastErrorReason { get; set; }
    public string? GroupKey { get; set; }
    public string? ImportType { get; set; }
    public DateTimeOffset? BatchDate { get; set; }

    public FileImport()
    {
    }

    private FileImport(
        string destinationTableName,
        string fileName,
        long totalRowsToProcess,
        long rowsFound)
    {
        DestinationTableName = destinationTableName;
        FileName = fileName.NormalizeToUpper()!;

        TotalRowsToProcess = totalRowsToProcess;
        RowsFound = rowsFound;

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
            throw new InvalidOperationException($"Invalid import status transition from {ImportStatus} to Failed. Use MarkFailed(reason) instead.");
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
        BusinessRuleChecker.CheckRule(new MarkTransferredRule(ImportStatus));

        ImportStatus = FileImportStatus.Transferred;
        ImportStartAt = DateTimeOffset.UtcNow;
    }

    public void MarkSplit()
    {
        BusinessRuleChecker.CheckRule(new MarkSplitRule(ImportStatus));

        ImportStatus = FileImportStatus.Split;
    }

    public void MarkCompleted()
    {
        BusinessRuleChecker.CheckRule(new MarkCompletedRule(ImportStatus));

        ImportStatus = FileImportStatus.Completed;
        ImportEndAt = DateTimeOffset.UtcNow;
    }

    public void MarkFailed(string reason, bool isTransient = true)
    {
        BusinessRuleChecker.CheckRule(new MarkFailedRule(ImportStatus));

        ImportStatus = FileImportStatus.Failed;
        ImportStartAt ??= DateTimeOffset.UtcNow;
        ImportEndAt = DateTimeOffset.UtcNow;

        LastErrorReason = reason;
        FailedAttempts = isTransient ? FailedAttempts + 1 : 3;
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
    }

    public void ForceResetImportStatus(FileImportStatus importStatus)
    {
        ImportStatus = importStatus;
        ProcessingStatus = FileProcessingStatus.Pending;
    }
}