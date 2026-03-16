using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories;

public abstract class ReadOnlyRepository<TEntity>(ICadsDbContext dbContext) : IReadOnlyRepository<TEntity>
    where TEntity : class
{
    protected ICadsDbContext DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = "",
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;

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

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByKeyAsync(params object[] keyValues)
    {
        var entity = await DbSet.FindAsync(keyValues);

        if (entity != null)
        {
            dbContext.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }
}