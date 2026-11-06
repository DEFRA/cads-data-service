using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using System.Collections.Concurrent;

namespace Cads.Cds.MiBff.Testing.Support.Fakes.Repositories;

/// <summary>
/// In-memory IMiReportJobRepository test double. Needed because the real
/// MiReportJobRepository.ClaimPendingOutboxMessagesAsync uses FromSqlRaw against a Postgres
/// function which the EF Core InMemory provider used by component tests cannot execute.
/// </summary>
public class FakeMiReportJobRepository : IMiReportJobRepository
{
    private readonly ConcurrentDictionary<Guid, MiReportJob> _jobs = new();
    private readonly ConcurrentQueue<MiReportJobOutbox> _outbox = new();

    public List<(Guid OutboxId, string Error, int MaxAttempts)> FailedCalls { get; } = [];

    public List<Guid> DeletedOutboxIds { get; } = [];

    public void SeedOutbox(MiReportJobOutbox outboxMessage) => _outbox.Enqueue(outboxMessage);

    public Task CreateJobWithOutboxAsync(
        MiReportJob reportJob,
        MiReportJobOutbox outboxMessage,
        CancellationToken cancellationToken = default)
    {
        _jobs[reportJob.ReportJobId] = reportJob;
        _outbox.Enqueue(outboxMessage);
        return Task.CompletedTask;
    }

    public Task<MiReportJob?> GetByReportJobIdAsync(Guid reportJobId, CancellationToken cancellationToken = default)
        => Task.FromResult(_jobs.GetValueOrDefault(reportJobId));

    public Task UpdateStatusAsync(
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
        if (_jobs.TryGetValue(reportJobId, out var job))
        {
            job.Status = status;
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<MiReportJobOutbox>> GetPendingOutboxMessagesAsync(
        int batchSize = 50,
        CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyList<MiReportJobOutbox>)_outbox.Take(batchSize).ToList());

    public Task<IReadOnlyList<MiReportJobOutbox>> ClaimPendingOutboxMessagesAsync(
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        var claimed = new List<MiReportJobOutbox>();

        while (claimed.Count < batchSize && _outbox.TryDequeue(out var outboxMessage))
        {
            claimed.Add(outboxMessage);
        }

        return Task.FromResult((IReadOnlyList<MiReportJobOutbox>)claimed);
    }

    public Task MarkOutboxDispatchFailedAsync(
        Guid outboxId,
        string error,
        int maxAttempts,
        CancellationToken cancellationToken = default)
    {
        FailedCalls.Add((outboxId, error, maxAttempts));
        return Task.CompletedTask;
    }

    public Task DeleteDispatchedOutboxMessagesAsync(
        IEnumerable<Guid> outboxIds,
        CancellationToken cancellationToken = default)
    {
        DeletedOutboxIds.AddRange(outboxIds);
        return Task.CompletedTask;
    }
}

