using Cads.Cds.SystemAdmin.Application.Promotion.Channels;
using Cads.Cds.SystemAdmin.Application.Promotion.Jobs;
using Cads.Cds.SystemAdmin.Core.Promotion;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;

namespace Cads.Cds.SystemAdmin.Infrastructure.Promotion.Channels;

/// <summary>
/// In-memory single-slot queue. Only one prepare job can be queued or running per instance;
/// the database also guards against concurrent plans across instances.
/// </summary>
public class PromotionJobQueue : IPromotionJobQueue
{
    private readonly Channel<PromotionJob> _channel =
        Channel.CreateBounded<PromotionJob>(new BoundedChannelOptions(1) { SingleReader = true });

    private int _busy;

    public bool TryEnqueue(PromotionType promotionType, bool? deleteSource, string? requestedBy)
    {
        if (Interlocked.CompareExchange(ref _busy, 1, 0) != 0)
        {
            return false;
        }

        if (_channel.Writer.TryWrite(new PromotionJob(promotionType, deleteSource, requestedBy)))
        {
            return true;
        }

        Volatile.Write(ref _busy, 0);
        return false;
    }

    public IAsyncEnumerable<PromotionJob> ReadAllAsync(CancellationToken cancellationToken) =>
        _channel.Reader.ReadAllAsync(cancellationToken);

    /// <summary>Releases the slot so another job can be queued.</summary>
    public void MarkCompleted() => Volatile.Write(ref _busy, 0);
}
