using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Contexts;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Persistence.Uow;

public class ManualUnitOfWorkTests
{
    [Fact]
    public async Task BeginTransaction_SetsIsInTransaction_AndCreatesTransaction()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        uow.IsInTransaction.Should().BeFalse();

        await uow.BeginTransactionAsync(TestContext.Current.CancellationToken);

        uow.IsInTransaction.Should().BeTrue();
        dbContext.Transaction.Should().NotBeNull();
    }

    [Fact]
    public async Task BeginTransaction_WhenAlreadyInTransaction_Throws()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        await uow.BeginTransactionAsync(TestContext.Current.CancellationToken);

        Func<Task> act = async () => await uow.BeginTransactionAsync(TestContext.Current.CancellationToken);

        await act.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("*already active*");
    }

    [Fact]
    public async Task Commit_CommitsTransaction_AndClearsState()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        await uow.BeginTransactionAsync(TestContext.Current.CancellationToken);
        await uow.SaveChangesAsync(TestContext.Current.CancellationToken);
        await uow.CommitAsync(TestContext.Current.CancellationToken);

        uow.IsInTransaction.Should().BeFalse();

        dbContext.Transaction!.Committed.Should().BeTrue();
        dbContext.Transaction.RolledBack.Should().BeFalse();
    }

    [Fact]
    public async Task Commit_WithoutTransaction_Throws()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        Func<Task> act = async () => await uow.CommitAsync();

        await act.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("*No active transaction*");
    }

    [Fact]
    public async Task Rollback_RollsBackTransaction_AndClearsState()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        await uow.BeginTransactionAsync(TestContext.Current.CancellationToken);
        await uow.RollbackAsync(TestContext.Current.CancellationToken);

        uow.IsInTransaction.Should().BeFalse();

        dbContext.Transaction!.RolledBack.Should().BeTrue();
        dbContext.Transaction.Committed.Should().BeFalse();
    }

    [Fact]
    public async Task Rollback_WithoutTransaction_DoesNothing()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        Func<Task> act = async () => await uow.RollbackAsync();

        await act.Should().NotThrowAsync();

        uow.IsInTransaction.Should().BeFalse();
        dbContext.Transaction.Should().BeNull();
    }

    [Fact]
    public async Task DisposeAsync_DisposesActiveTransaction()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        await uow.BeginTransactionAsync(TestContext.Current.CancellationToken);
        await uow.DisposeAsync();

        uow.IsInTransaction.Should().BeFalse();
    }

    [Fact]
    public async Task SaveChanges_DelegatesToDbContext()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        await uow.SaveChangesAsync(TestContext.Current.CancellationToken);

        dbContext.SaveChangesCalled.Should().BeTrue();
    }
}