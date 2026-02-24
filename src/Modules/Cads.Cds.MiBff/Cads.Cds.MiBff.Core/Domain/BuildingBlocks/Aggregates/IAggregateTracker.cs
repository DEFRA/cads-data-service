namespace Cads.Cds.MiBff.Core.Domain.BuildingBlocks.Aggregates;

public interface IAggregateTracker
{
    void Track(IAggregateRoot aggregate);
    IEnumerable<IAggregateRoot> GetTrackedAggregates();
    void Clear();
}