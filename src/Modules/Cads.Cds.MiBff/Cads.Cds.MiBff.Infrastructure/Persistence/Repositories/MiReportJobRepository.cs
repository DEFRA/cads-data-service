using Cads.Cds.MiBff.Application.Uow;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiReportJobRepository(MiBffWriteDbContext writeDbContext, IMiBffUnitOfWork uow)
    : IMiReportJobRepository
{
    public async Task CreateJobWithOutboxAsync(
        MiReportJob reportJob,
        MiReportJobOutbox outboxMessage,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(reportJob);
        ArgumentNullException.ThrowIfNull(outboxMessage);

        await uow.ExecuteInTransactionAsync(async ct =>
        {
            await writeDbContext.ReportJobs.AddAsync(reportJob, ct);
            await writeDbContext.ReportJobOutboxMessages.AddAsync(outboxMessage, ct);
            return true;
        }, cancellationToken);
    }

    public async Task<MiReportJob?> GetByReportJobIdAsync(Guid reportJobId, CancellationToken cancellationToken = default)
    {
        return await writeDbContext.ReportJobs
            .AsNoTracking()
            .SingleOrDefaultAsync(j => j.ReportJobId == reportJobId, cancellationToken);
    }

    public async Task UpdateStatusAsync(
        Guid reportJobId,
        string status,
        string? s3Key = null,
        int? rowCount = null,
        string? error = null,
        DateTimeOffset? startedAt = null,
        DateTimeOffset? completedAt = null,
        DateTimeOffset? expiresAt = null,
        CancellationToken cancellationToken = default)
    {
        var job = await writeDbContext.ReportJobs
            .SingleOrDefaultAsync(j => j.ReportJobId == reportJobId, cancellationToken)
            ?? throw new InvalidOperationException($"MiReportJob with ReportJobId {reportJobId} not found.");

        job.Status = status;
        job.S3Key ??= s3Key;
        job.RowCount ??= rowCount;
        job.Error = error ?? job.Error;
        job.StartedAt ??= startedAt;
        job.CompletedAt ??= completedAt;
        job.ExpiresAt ??= expiresAt;
        job.UpdatedAt = DateTimeOffset.UtcNow;

        await writeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MiReportJobOutbox>> GetPendingOutboxMessagesAsync(
        int batchSize = 50,
        CancellationToken cancellationToken = default)
    {
        return await writeDbContext.ReportJobOutboxMessages
            .AsNoTracking()
            .Where(o => o.Status == ReportJobOutboxStatus.Pending)
            .OrderBy(o => o.CreatedAt)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MiReportJobOutbox>> ClaimPendingOutboxMessagesAsync(
        int batchSize = 20,
        CancellationToken cancellationToken = default)
    {
        // Delegates the claim+lock+flip logic to the cads.claim_pending_report_job_outbox_messages
        // Postgres function (see 0000_013_mi_report_job_tables.postgresql.sql), which uses
        // FOR UPDATE SKIP LOCKED internally so concurrent dispatcher replicas never claim the
        // same pending row. No explicit transaction needed - the function body runs atomically.
        const string sql = "SELECT * FROM cads.claim_pending_report_job_outbox_messages({0})";

        return await writeDbContext.ReportJobOutboxMessages
            .FromSqlRaw(sql, batchSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task MarkOutboxDispatchFailedAsync(
        Guid outboxId,
        string error,
        int maxAttempts,
        CancellationToken cancellationToken = default)
    {
        var outboxMessage = await writeDbContext.ReportJobOutboxMessages
            .SingleOrDefaultAsync(o => o.OutboxId == outboxId, cancellationToken);

        if (outboxMessage is null)
            return;

        outboxMessage.Attempts += 1;
        outboxMessage.LastError = error.Length > 2000 ? error[..2000] : error;
        outboxMessage.Status = outboxMessage.Attempts >= maxAttempts
            ? ReportJobOutboxStatus.Failed
            : ReportJobOutboxStatus.Pending;

        await writeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteDispatchedOutboxMessagesAsync(
        IEnumerable<Guid> outboxIds,
        CancellationToken cancellationToken = default)
    {
        var ids = outboxIds.ToList();

        if (ids.Count == 0)
        {
            return;
        }

        var toRemove = await writeDbContext.ReportJobOutboxMessages
            .Where(o => ids.Contains(o.OutboxId))
            .ToListAsync(cancellationToken);

        writeDbContext.ReportJobOutboxMessages.RemoveRange(toRemove);
        await writeDbContext.SaveChangesAsync(cancellationToken);
    }
}