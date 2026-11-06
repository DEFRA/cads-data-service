namespace Cads.Cds.BuildingBlocks.Core.Domain.Outbox;

/// <summary>
/// Marker contract for an outbox table row that can be dispatched by a
/// Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers.BaseOutboxDispatcher{TClient,TRepository,TOutbox,TMessage}.
/// </summary>
public interface IOutboxMessage
{
    Guid OutboxId { get; }
}