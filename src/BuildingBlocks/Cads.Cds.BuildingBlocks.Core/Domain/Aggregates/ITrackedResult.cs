namespace Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;

public interface ITrackedResult
{
    IReadOnlyCollection<IAggregateRoot> Aggregates { get; }
}