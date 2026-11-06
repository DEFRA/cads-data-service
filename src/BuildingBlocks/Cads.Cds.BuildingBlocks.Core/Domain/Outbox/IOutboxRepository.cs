namespace Cads.Cds.BuildingBlocks.Core.Domain.Outbox;

/// <summary>
/// Repository contract required by BaseOutboxDispatcher to claim, fail and delete outbox rows.
/// Module-specific repositories (e.g. IMiReportJobRepository) extend this interface so the generic
/// dispatch loop can be shared across modules.
/// </summary>
public interface IOutboxRepository<TOutbox>
    where TOutbox : IOutboxMessage
{
    Task<IReadOnlyList<TOutbox>> ClaimPendingOutboxMessagesAsync(
        int batchSize,
        CancellationToken cancellationToken = default);

    Task MarkOutboxDispatchFailedAsync(
        Guid outboxId,
        string error,
        int maxAttempts,
        CancellationToken cancellationToken = default);

    Task DeleteDispatchedOutboxMessagesAsync(
        IEnumerable<Guid> outboxIds,
        CancellationToken cancellationToken = default);
}

