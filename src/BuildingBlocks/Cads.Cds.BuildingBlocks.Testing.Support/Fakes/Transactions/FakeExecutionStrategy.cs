using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;

public class FakeExecutionStrategy : IExecutionStrategy
{
    public bool RetriesOnFailure => throw new NotImplementedException();

    public static Task<T> ExecuteAsync<T>(Func<Task<T>> operation)
        => operation();

    public static Task ExecuteAsync(Func<Task> operation)
        => operation();

    public static T Execute<T>(Func<T> operation)
        => operation();

    public static void Execute(Action operation)
        => operation();

    public TResult Execute<TState, TResult>(TState state, Func<DbContext, TState, TResult> operation, Func<DbContext, TState, ExecutionResult<TResult>>? verifySucceeded)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> ExecuteAsync<TState, TResult>(TState state, Func<DbContext, TState, CancellationToken, Task<TResult>> operation, Func<DbContext, TState, CancellationToken, Task<ExecutionResult<TResult>>>? verifySucceeded, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}