using Cads.Cds.BuildingBlocks.Core.Domain.Entities;
using Cads.Cds.BuildingBlocks.Core.Domain.Events;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}