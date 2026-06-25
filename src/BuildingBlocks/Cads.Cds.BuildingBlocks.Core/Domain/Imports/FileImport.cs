using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports;

public class FileImport
{
    public long Id { get; private set; }

    public string DestinationTableName { get; private set; } = default!;
    public string FileName { get; private set; } = default!;

    public long TotalRowsToProcess { get; private set; }
    public long RowsFound { get; private set; }

    public FileImportStatus ImportStatus { get; private set; }
    public FileProcessingStatus ProcessingStatus { get; private set; }

    public DateTimeOffset AddedAt { get; private set; }
    public DateTimeOffset? ImportStartAt { get; private set; }
    public DateTimeOffset? ImportEndAt { get; private set; }
    public DateTimeOffset? ProcessingStartAt { get; private set; }
    public DateTimeOffset? ProcessingEndAt { get; private set; }

    private FileImport() { }

    private FileImport(
        string destinationTableName,
        string fileName,
        long totalRowsToProcess,
        long rowsFound)
    {
        DestinationTableName = destinationTableName;
        FileName = fileName;

        TotalRowsToProcess = totalRowsToProcess;
        RowsFound = rowsFound;

        ImportStatus = FileImportStatus.Pending;
        ProcessingStatus = FileProcessingStatus.Pending;

        AddedAt = DateTimeOffset.UtcNow;
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

    // -----------------------------
    // Importing workflow
    // -----------------------------

    public void MarkImporting()
    {
        if (ImportStatus != FileImportStatus.Pending)
            throw new DomainException("Importing can only start from Pending.");

        ImportStatus = FileImportStatus.Importing;
        ImportStartAt = DateTimeOffset.UtcNow;
    }

    public void MarkImportComplete()
    {
        if (ImportStatus != FileImportStatus.Importing)
            throw new DomainException("Import must be in Importing state to complete.");

        ImportStatus = FileImportStatus.Complete;
        ImportEndAt = DateTimeOffset.UtcNow;
    }

    public void MarkImportFailed()
    {
        ImportStatus = FileImportStatus.Failed;
        ImportEndAt = DateTimeOffset.UtcNow;
    }

    // -----------------------------
    // Processing workflow
    // -----------------------------

    public void MarkProcessingStarted()
    {
        if (ProcessingStatus != FileProcessingStatus.Pending)
            throw new DomainException("Processing can only start from Pending.");

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
}
