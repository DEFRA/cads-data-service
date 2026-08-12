using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

public static class BusinessRuleChecker
{
    public static void CheckRule(params IBusinessRule[] rules)
    {
        foreach (var rule in rules.Where(rule => rule.IsBroken()))
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}