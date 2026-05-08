namespace Cads.Cds.BuildingBlocks.Testing.Support.Persistence;

public class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner = inner;

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> MoveNextAsync()
        => new(_inner.MoveNext());

    public T Current => _inner.Current;
}