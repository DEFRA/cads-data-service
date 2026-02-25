namespace Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;

public class TrackedResult<T>(T result, params IAggregateRoot[] aggregates) : ITrackedResult
{
    public T Result { get; } = result;
    public IReadOnlyCollection<IAggregateRoot> Aggregates { get; } = aggregates;
}