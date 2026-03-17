namespace Cads.Cds.BuildingBlocks.Core.Persistence;

public interface IReadOnlyRepository<T>
    where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAsync(
        Func<IQueryable<T>, IQueryable<T>>? queryShaper = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(
        Func<IQueryable<T>, IQueryable<TResult>> projection,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);
}