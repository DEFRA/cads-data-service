using Cads.Cds.BuildingBlocks.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;

public abstract class EFRepository<TEntity, TDbContext>(TDbContext dbContext)
    : EFReadOnlyRepository<TEntity, TDbContext>(dbContext), IRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    public async Task AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}