using Cads.Cds.SystemAdmin.Application.Promotion.Jobs;
using Cads.Cds.SystemAdmin.Core.Promotion;

namespace Cads.Cds.SystemAdmin.Application.Promotion.Factories;

public interface IPromotionJobProcessorStrategy
{
    PromotionType PromotionType { get; }
    Task<long> PrepareAsync(PromotionJob job, CancellationToken cancellationToken);
}