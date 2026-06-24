using Microsoft.EntityFrameworkCore.Storage;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;

public class FakeDbTransaction : IDbContextTransaction
{
    public bool Committed { get; private set; }
    public bool RolledBack { get; private set; }

    public Guid TransactionId => Guid.NewGuid();

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        Committed = true;
        return Task.CompletedTask;
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        RolledBack = true;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public void Commit()
    {
        throw new NotImplementedException();
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }
}