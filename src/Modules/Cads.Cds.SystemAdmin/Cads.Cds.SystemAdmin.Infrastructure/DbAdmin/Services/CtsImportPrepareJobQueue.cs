using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;

public sealed record CtsImportPrepareJob(string? RequestedBy);

/// <summary>
/// In-memory single-slot queue. Only one prepare job can be queued or running per instance;
/// the database also guards against concurrent plans across instances.
/// </summary>
public sealed class CtsImportPrepareJobQueue : ICtsImportPrepareJobQueue
{
    private readonly Channel<CtsImportPrepareJob> _channel =
        Channel.CreateBounded<CtsImportPrepareJob>(new BoundedChannelOptions(1) { SingleReader = true });

    private int _busy;

    public bool TryEnqueue(string? requestedBy)
    {
        if (Interlocked.CompareExchange(ref _busy, 1, 0) != 0)
        {
            return false;
        }

        if (_channel.Writer.TryWrite(new CtsImportPrepareJob(requestedBy)))
        {
            return true;
        }

        Volatile.Write(ref _busy, 0);
        return false;
    }

    public IAsyncEnumerable<CtsImportPrepareJob> ReadAllAsync(CancellationToken cancellationToken) =>
        _channel.Reader.ReadAllAsync(cancellationToken);

    /// <summary>Releases the slot so another job can be queued.</summary>
    public void MarkCompleted() => Volatile.Write(ref _busy, 0);
}