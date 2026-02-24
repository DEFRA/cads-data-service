namespace Cads.Cds.MiBff.Core.Domain.BuildingBlocks.Aggregates;

public interface ITrackedResult
{
    IReadOnlyCollection<IAggregateRoot> Aggregates { get; }
}