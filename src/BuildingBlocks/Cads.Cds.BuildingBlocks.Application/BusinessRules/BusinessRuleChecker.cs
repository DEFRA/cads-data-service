using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

namespace Cads.Cds.BuildingBlocks.Application.BusinessRules;

public static class BusinessRuleChecker
{
    public static void CheckRule(params IBusinessRule[] rules)
    {
        foreach (var rule in rules)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}