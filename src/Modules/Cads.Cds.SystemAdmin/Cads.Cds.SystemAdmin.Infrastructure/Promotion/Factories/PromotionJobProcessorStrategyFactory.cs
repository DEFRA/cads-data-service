using Cads.Cds.SystemAdmin.Application.Promotion.Factories;
using Cads.Cds.SystemAdmin.Core.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Promotion.Factories;

public class PromotionJobProcessorStrategyFactory(IEnumerable<IPromotionJobProcessorStrategy> strategies)
    : IPromotionJobProcessorStrategyFactory
{
    public IPromotionJobProcessorStrategy GetStrategy(PromotionType promotionType)
    {
        return strategies.FirstOrDefault(s => s.PromotionType == promotionType)
            ?? throw new InvalidOperationException($"No strategy found for PromotionType: {promotionType}");
    }
}