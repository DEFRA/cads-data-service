using Cads.Cds.MiBff.Core.Repositories;

namespace Cads.Cds.MiBff.Core.Domain.BuildingBlocks.Aggregates;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}