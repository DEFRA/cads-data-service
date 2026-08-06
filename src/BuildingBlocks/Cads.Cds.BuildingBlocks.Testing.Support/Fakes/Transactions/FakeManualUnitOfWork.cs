using Cads.Cds.BuildingBlocks.Application.Uow;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;

public class FakeManualUnitOfWork<TDbContext>(TDbContext dbContext) : IManualUnitOfWork
    where TDbContext : CadsDbContext
{
    public async Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<TResult>> operation,
        CancellationToken cancellationToken = default)
    {
        var result = await operation(cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}