using Cads.Cds.SystemAdmin.Core.Promotion;

namespace Cads.Cds.SystemAdmin.Application.Promotion.Factories;

public interface IPromotionJobProcessorStrategyFactory
{
    IPromotionJobProcessorStrategy GetStrategy(PromotionType promotionType);
}