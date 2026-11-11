using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.MiBff.Application.Reports.Jobs;

/// <summary>
/// Runs a report job end to end: builds the query, generates the report, stores the encrypted
/// output and records the job status transitions (pending → processing → complete | failed).
/// </summary>
public class ReportJobProcessor(
    IMiReportJobRepository reportJobRepository,
    IReportJobQueryBuilder queryBuilder,
    IMediator mediator,
    IReportFileStore reportFileStore,
    ILogger<ReportJobProcessor> logger)
    : IReportJobProcessor
{
    public async Task ProcessAsync(Guid reportJobId, CancellationToken cancellationToken = default)
    {
        var job = await reportJobRepository.GetByReportJobIdAsync(reportJobId, cancellationToken)
            ?? throw new NonRetryableException($"Report job {reportJobId} was not found.");

        if (IsFinalised(job))
        {
            logger.LogInformation(
                "Skipping report job {ReportJobId} as it is already in status {Status}.",
                job.ReportJobId, job.Status);

            return;
        }

        try
        {
            var storageKey = await GenerateAndStoreAsync(job, cancellationToken);

            await reportJobRepository.UpdateStatusAsync(
                job.ReportJobId,
                ReportJobStatus.Complete,
                s3Key: storageKey,
                completedAt: DateTimeOffset.UtcNow,
                cancellationToken: cancellationToken);

            logger.LogInformation(
                "Report job {ReportJobId} for {ReportKey} completed and stored at {StorageKey}.",
                job.ReportJobId, job.ReportKey, storageKey);
        }
        catch (Exception ex) when (ex is not RetryableException and not OperationCanceledException)
        {
            // Retryable/cancelled failures leave the job in Processing so redelivery can pick it up.
            await MarkFailedAsync(job.ReportJobId, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Pending: first delivery. Processing: redelivery after a retryable failure.
    /// Anything else has already been finalised, so the message is a duplicate.
    /// </summary>
    private static bool IsFinalised(MiReportJob job) =>
        job.Status is not (ReportJobStatus.Pending or ReportJobStatus.Processing);

    private async Task<string> GenerateAndStoreAsync(MiReportJob job, CancellationToken cancellationToken)
    {
        var query = queryBuilder.Build(job);

        await reportJobRepository.UpdateStatusAsync(
            job.ReportJobId,
            ReportJobStatus.Processing,
            startedAt: DateTimeOffset.UtcNow,
            cancellationToken: cancellationToken);

        var file = await RunQueryAsync(query, job.ReportKey, cancellationToken);

        await using (file.Stream)
        {
            var storageKey = ReportStorageKeys.For(query.GetType(), job.ReportJobId);

            await reportFileStore.SaveAsync(storageKey, file.Stream, cancellationToken);

            return storageKey;
        }
    }

    private async Task<FileReportResult> RunQueryAsync(object query, string reportKey, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return result as FileReportResult
            ?? throw new NonRetryableException(
                $"Report '{reportKey}' returned {result?.GetType().Name ?? "null"}; expected {nameof(FileReportResult)}.");
    }

    private Task MarkFailedAsync(Guid reportJobId, string error) =>
        // Not tied to the message's cancellation token: the failure must be recorded.
        reportJobRepository.UpdateStatusAsync(
            reportJobId,
            ReportJobStatus.Failed,
            error: error,
            completedAt: DateTimeOffset.UtcNow,
            cancellationToken: CancellationToken.None);
}
