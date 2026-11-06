namespace Cads.Cds.BuildingBlocks.Core.Domain.Outbox;

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