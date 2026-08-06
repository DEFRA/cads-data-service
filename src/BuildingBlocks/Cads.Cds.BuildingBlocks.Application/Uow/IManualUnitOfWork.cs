namespace Cads.Cds.BuildingBlocks.Application.Uow;

public interface IManualUnitOfWork : IAsyncDisposable
{
    Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<TResult>> operation,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}