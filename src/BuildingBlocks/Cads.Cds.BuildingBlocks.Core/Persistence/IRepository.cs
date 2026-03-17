namespace Cads.Cds.BuildingBlocks.Core.Persistence;

public interface IRepository<T> : IReadOnlyRepository<T>
    where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Remove(T entity);
}