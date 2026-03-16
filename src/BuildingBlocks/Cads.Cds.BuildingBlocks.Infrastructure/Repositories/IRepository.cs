namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories;

public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>
    where TEntity : class
    where TKey : IEquatable<TKey>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteByKeyAsync(TKey key, CancellationToken cancellationToken);

    void Update(TEntity entity);
}