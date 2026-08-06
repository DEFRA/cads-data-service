using Cads.Cds.BuildingBlocks.Application.Uow;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;

public class ManualUnitOfWork<TDbContext>(TDbContext dbContext) : IManualUnitOfWork
    where TDbContext : CadsDbContext
{
    private readonly TDbContext _dbContext = dbContext;

    private IDbContextTransaction? _transaction;

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<TResult>> operation,
        CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _dbContext.BeginTransactionAsync(cancellationToken);
            _transaction = tx;

            try
            {
                var result = await operation(cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);

                return result;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                _transaction = null;
            }
        });
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
            GC.SuppressFinalize(this);
        }
    }
}