using Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;
using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

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