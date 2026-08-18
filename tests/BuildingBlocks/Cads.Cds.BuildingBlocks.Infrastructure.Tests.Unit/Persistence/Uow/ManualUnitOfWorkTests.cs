using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Contexts;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Persistence.Uow;

public class ManualUnitOfWorkTests
{
    [Fact]
    public async Task ExecuteInTransactionAsync_WhenOperationSucceeds_CommitsAndReturnsResult()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        var result = await uow.ExecuteInTransactionAsync(
            _ => Task.FromResult(42),
            TestContext.Current.CancellationToken);

        result.Should().Be(42);

        dbContext.Transaction!.Committed.Should().BeTrue();
        dbContext.Transaction.RolledBack.Should().BeFalse();
        dbContext.SaveChangesCalled.Should().BeTrue();
    }

    [Fact]
    public async Task ExecuteInTransactionAsync_WhenOperationSucceeds_InvokesOperationExactlyOnce()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        var callCount = 0;

        await uow.ExecuteInTransactionAsync(
            _ =>
            {
                callCount++;
                return Task.FromResult(0);
            },
            TestContext.Current.CancellationToken);

        callCount.Should().Be(1);
    }

    [Fact]
    public async Task ExecuteInTransactionAsync_WhenOperationThrows_RollsBackAndRethrows()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        Func<Task> act = () => uow.ExecuteInTransactionAsync<int>(
            _ => throw new InvalidOperationException("boom"),
            TestContext.Current.CancellationToken);

        await act.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("boom");

        dbContext.Transaction!.RolledBack.Should().BeTrue();
        dbContext.Transaction.Committed.Should().BeFalse();
        dbContext.SaveChangesCalled.Should().BeFalse();
    }

    [Fact]
    public async Task SaveChangesAsync_DelegatesToDbContext()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        await uow.SaveChangesAsync(TestContext.Current.CancellationToken);

        dbContext.SaveChangesCalled.Should().BeTrue();
    }

    [Fact]
    public async Task DisposeAsync_WithoutActiveTransaction_DoesNotThrow()
    {
        var dbContext = new FakeWriteDbContext();
        var uow = new ManualUnitOfWork<FakeWriteDbContext>(dbContext);

        Func<Task> act = async () => await uow.DisposeAsync();

        await act.Should().NotThrowAsync();
    }
}