using Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;
using Cads.Cds.BuildingBlocks.Core.Domain.Events;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Livestock;

[ExcludeFromCodeCoverage]
public class Animal : IAggregateRoot
{
    public string Id { get; set; } = string.Empty;

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void ClearDomainEvents() => _domainEvents.Clear();
}