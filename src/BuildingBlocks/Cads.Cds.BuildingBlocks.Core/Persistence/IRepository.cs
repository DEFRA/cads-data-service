namespace Cads.Cds.BuildingBlocks.Core.Persistence;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
    where TEntity : class
{
    Task AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken);

    Task Remove(TEntity entity, CancellationToken cancellationToken);
}