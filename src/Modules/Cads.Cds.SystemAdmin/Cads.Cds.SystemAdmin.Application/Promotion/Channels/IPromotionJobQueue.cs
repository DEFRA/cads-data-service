using Cads.Cds.SystemAdmin.Core.Promotion;

namespace Cads.Cds.SystemAdmin.Application.Promotion.Channels;

public interface IPromotionJobQueue
{
    /// <summary>
    /// Queues a parallel bulk or delta import prepare job for background execution.
    /// </summary>
    /// <param name="requestedBy">The user who requested the job, used for logging.</param>
    /// <returns>
    /// <c>false</c> if a prepare job is already queued or running in this instance (nothing is queued).
    /// </returns>
    bool TryEnqueue(PromotionType promotionType, bool? deleteSource, string? requestedBy);
}
