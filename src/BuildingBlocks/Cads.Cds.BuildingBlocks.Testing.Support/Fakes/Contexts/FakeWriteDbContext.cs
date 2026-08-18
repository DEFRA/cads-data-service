using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Contexts;

public class FakeWriteDbContext : CadsDbContext
{
    public FakeDbTransaction? Transaction { get; private set; }
    public bool SaveChangesCalled { get; private set; }

    public FakeWriteDbContext()
        : base(new DbContextOptionsBuilder<FakeWriteDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveChangesCalled = true;
        return Task.FromResult(1);
    }

    public override Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        Transaction = new FakeDbTransaction();
        return Task.FromResult<IDbContextTransaction>(Transaction);
    }

    public override IExecutionStrategy CreateExecutionStrategy()
        => new FakeExecutionStrategy();
}