namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;

public interface IManualUnitOfWork : IAsyncDisposable
{
    bool IsInTransaction { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}