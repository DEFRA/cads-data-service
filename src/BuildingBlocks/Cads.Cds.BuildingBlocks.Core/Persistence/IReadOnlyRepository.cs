using System.Linq.Expressions;

namespace Cads.Cds.BuildingBlocks.Core.Persistence;

public interface IReadOnlyRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByKeyAsync(params object[] keyValues);

    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
       string? includeProperties = "",
       bool asNoTracking = true,
       CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAsync(
      Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryShaper = null,
      bool asNoTracking = true,
      CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> projection,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);
}