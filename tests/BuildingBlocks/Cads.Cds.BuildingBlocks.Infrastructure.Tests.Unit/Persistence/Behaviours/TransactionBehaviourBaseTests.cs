using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Contexts;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Persistence.Behaviours;

public class TransactionBehaviourBaseTests
{
    [Fact]
    public async Task TransactionalCommand_CommitsTransaction()
    {
        var db = new FakeWriteDbContext();
        var behaviour = new TestTransactionBehaviour<TestTransactionalCommand, string>(db);

        var result = await behaviour.Handle(
            new TestTransactionalCommand(),
            (cancellationToken) => Task.FromResult("OK"),
            CancellationToken.None);

        result.Should().Be("OK");
        db.SaveChangesCalled.Should().BeTrue();
        db.Transaction.Should().NotBeNull();
        db.Transaction!.Committed.Should().BeTrue();
        db.Transaction.RolledBack.Should().BeFalse();
    }

    [Fact]
    public async Task NonTransactionalCommand_DoesNotStartTransaction()
    {
        var db = new FakeWriteDbContext();
        var behaviour = new TestTransactionBehaviour<TestNonTransactionalCommand, string>(db);

        var result = await behaviour.Handle(
            new TestNonTransactionalCommand(),
            (cancellationToken) => Task.FromResult("OK"),
            CancellationToken.None);

        result.Should().Be("OK");
        db.SaveChangesCalled.Should().BeFalse();
        db.Transaction.Should().BeNull();
    }

    [Fact]
    public async Task TransactionalCommand_Exception_RollsBack()
    {
        var db = new FakeWriteDbContext();
        var behaviour = new TestTransactionBehaviour<TestTransactionalCommand, string>(db);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            behaviour.Handle(
                new TestTransactionalCommand(),
                (cancellationToken) => throw new InvalidOperationException("Boom"),
                CancellationToken.None));

        db.Transaction.Should().NotBeNull();
        db.Transaction!.Committed.Should().BeFalse();
        db.Transaction.RolledBack.Should().BeTrue();
        db.SaveChangesCalled.Should().BeFalse();
    }

    public record TestTransactionalCommand()
        : ICommand<string>, ITransactionalCommand;

    public record TestNonTransactionalCommand()
        : ICommand<string>;
}