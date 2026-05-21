using Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;
using Cads.Cds.BuildingBlocks.Core.Domain.Events;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Livestock;

public class Location : IAggregateRoot
{
    public string Id { get; set; } = string.Empty;

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void ClearDomainEvents() => _domainEvents.Clear();
}