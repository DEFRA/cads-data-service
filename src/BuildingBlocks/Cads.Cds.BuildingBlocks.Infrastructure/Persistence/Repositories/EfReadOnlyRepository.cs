using Cads.Cds.BuildingBlocks.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;

public class EfReadOnlyRepository<T, TDbContext> : IReadOnlyRepository<T>
    where T : class
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;

    public EfReadOnlyRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected virtual IQueryable<T> Query(bool asNoTracking = true)
    {
        var query = DbContext.Set<T>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return query;
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Query().FirstOrDefaultAsync(
            e => EF.Property<Guid>(e, "Id") == id,
            cancellationToken);

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => Query().AnyAsync(
            e => EF.Property<Guid>(e, "Id") == id,
            cancellationToken);

    public async Task<IReadOnlyList<T>> ListAsync(
        Func<IQueryable<T>, IQueryable<T>>? queryShaper = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Query(asNoTracking);

        if (queryShaper is not null)
            query = queryShaper(query);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> ProjectAsync<TResult>(
        Func<IQueryable<T>, IQueryable<TResult>> projection,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> baseQuery = Query(asNoTracking);

        IQueryable<TResult> shaped = projection(baseQuery);

        return await shaped.ToListAsync(cancellationToken);
    }
}