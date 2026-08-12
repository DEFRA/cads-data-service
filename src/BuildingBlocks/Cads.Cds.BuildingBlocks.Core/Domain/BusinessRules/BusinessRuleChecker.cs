using Cads.Cds.BuildingBlocks.Core.Exceptions;

namespace Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

public static class BusinessRuleChecker
{
    public static void CheckRule(params IBusinessRule[] rules)
    {
        var brokenRule = rules.FirstOrDefault(rule => rule.IsBroken());
        if (brokenRule != null)
        {
            throw new BusinessRuleValidationException(brokenRule);
        }
    }
}