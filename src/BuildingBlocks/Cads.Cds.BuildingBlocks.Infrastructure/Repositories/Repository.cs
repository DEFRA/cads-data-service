using Cads.Cds.BuildingBlocks.Infrastructure.Database;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories;

public abstract class Repository<TEntity, TKey>(CadsDbContext dbContext)
    : ReadOnlyRepository<TEntity>(dbContext), IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : IEquatable<TKey>
{
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteByKeyAsync(TKey key, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FindAsync([key], cancellationToken);

        if (entity != null)
        {
            DbSet.Remove(entity);
        }
    }
}