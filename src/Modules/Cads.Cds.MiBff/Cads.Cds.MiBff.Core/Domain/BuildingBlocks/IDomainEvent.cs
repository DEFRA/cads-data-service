using MediatR;

namespace Cads.Cds.MiBff.Core.Domain.BuildingBlocks;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}