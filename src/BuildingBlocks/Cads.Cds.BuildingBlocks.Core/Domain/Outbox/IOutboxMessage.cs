namespace Cads.Cds.BuildingBlocks.Core.Domain.Outbox;

public interface IOutboxMessage
{
    Guid OutboxId { get; }
}