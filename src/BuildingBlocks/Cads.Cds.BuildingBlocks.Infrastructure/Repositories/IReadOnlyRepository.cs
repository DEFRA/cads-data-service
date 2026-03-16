using System.Linq.Expressions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories;

public interface IReadOnlyRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByKeyAsync(params object[] keyValues);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
       string? includeProperties = "", CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}