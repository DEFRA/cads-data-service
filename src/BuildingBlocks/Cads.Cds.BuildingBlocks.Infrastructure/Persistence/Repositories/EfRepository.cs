using Cads.Cds.BuildingBlocks.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;

public class EfRepository<T, TDbContext>
    : EfReadOnlyRepository<T, TDbContext>, IRepository<T>
    where T : class
    where TDbContext : DbContext
{
    public EfRepository(TDbContext dbContext) : base(dbContext) { }

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => DbContext.Set<T>().AddAsync(entity, cancellationToken).AsTask();

    public void Remove(T entity)
        => DbContext.Set<T>().Remove(entity);
}