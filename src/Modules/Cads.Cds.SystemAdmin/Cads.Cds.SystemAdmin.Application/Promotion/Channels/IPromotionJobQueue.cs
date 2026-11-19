using Cads.Cds.SystemAdmin.Application.Promotion.Jobs;
using Cads.Cds.SystemAdmin.Core.Promotion;

namespace Cads.Cds.SystemAdmin.Application.Promotion.Channels;

public interface IPromotionJobQueue
{
    bool TryEnqueue(PromotionType promotionType, string correlationId, bool? deleteSource, string? requestedBy);

    IAsyncEnumerable<PromotionJob> ReadAllAsync(CancellationToken cancellationToken);

    void MarkCompleted();
}
