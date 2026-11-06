using Cads.Cds.BuildingBlocks.Core.Domain.Outbox;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiReportJobRepository : IOutboxRepository<MiReportJobOutbox>
{
    /// <summary>
    /// Creates the report job and its outbox message atomically in a single transaction.
    /// </summary>
    Task CreateJobWithOutboxAsync(
        MiReportJob reportJob,
        MiReportJobOutbox outboxMessage,
        CancellationToken cancellationToken = default);

    Task<MiReportJob?> GetByReportJobIdAsync(Guid reportJobId, CancellationToken cancellationToken = default);

    Task UpdateStatusAsync(
        Guid reportJobId,
        string status,
        string? s3Key = null,
        int? rowCount = null,
        string? error = null,
        DateTimeOffset? startedAt = null,
        DateTimeOffset? completedAt = null,
        DateTimeOffset? expiresAt = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MiReportJobOutbox>> GetPendingOutboxMessagesAsync(
        int batchSize = 50,
        CancellationToken cancellationToken = default);

    // ClaimPendingOutboxMessagesAsync, MarkOutboxDispatchFailedAsync and
    // DeleteDispatchedOutboxMessagesAsync are inherited from IOutboxRepository<MiReportJobOutbox>.
}