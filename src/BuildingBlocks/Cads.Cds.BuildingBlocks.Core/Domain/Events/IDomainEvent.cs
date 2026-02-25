using MediatR;

namespace Cads.Cds.BuildingBlocks.Core.Domain.Events;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}