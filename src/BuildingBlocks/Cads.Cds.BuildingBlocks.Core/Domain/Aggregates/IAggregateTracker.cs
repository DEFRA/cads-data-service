namespace Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;

public interface IAggregateTracker
{
    void Track(IAggregateRoot aggregate);
    IEnumerable<IAggregateRoot> GetTrackedAggregates();
    void Clear();
}