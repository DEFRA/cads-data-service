using Cads.Cds.BuildingBlocks.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;

[ExcludeFromCodeCoverage]
public abstract class EFRepository<TEntity, TDbContext>(TDbContext dbContext)
    : EFReadOnlyRepository<TEntity, TDbContext>(dbContext), IRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.AddAsync(entity, cancellationToken);
    }

    public Task Remove(TEntity entity)
    {
        DbContext.Remove(entity);
        return Task.CompletedTask;
    }
}