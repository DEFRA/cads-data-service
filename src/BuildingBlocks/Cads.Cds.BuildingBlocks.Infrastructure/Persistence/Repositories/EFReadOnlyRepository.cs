using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;

public abstract class EFReadOnlyRepository<TEntity, TDbContext>(TDbContext dbContext)
    : IReadOnlyRepository<TEntity>
    where TEntity : class
    where TDbContext : CadsDbContext
{
    protected TDbContext DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    protected virtual IQueryable<TEntity> Query(bool asNoTracking = true)
    {
        var query = DbContext.Set<TEntity>().AsQueryable();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public async Task<TEntity?> GetByKeyAsync(params object[] keyValues)
    {
        var entity = await DbContext.Set<TEntity>().FindAsync(keyValues);

        if (entity != null)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Query().AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = "",
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = Query(asNoTracking);

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            query = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return orderBy is not null
            ? await orderBy(query).ToListAsync(cancellationToken)
            : await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryShaper = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = Query(asNoTracking);

        if (queryShaper is not null)
        {
            query = queryShaper(query);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> projection,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = Query(asNoTracking);

        var shaped = projection(baseQuery);

        return await shaped.ToListAsync(cancellationToken);
    }
}