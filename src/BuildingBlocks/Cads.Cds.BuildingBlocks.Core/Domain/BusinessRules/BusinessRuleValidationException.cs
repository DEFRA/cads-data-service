namespace Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

public class BusinessRuleValidationException(IBusinessRule brokenRule) : Exception(brokenRule.Message)
{
    private IBusinessRule BrokenRule { get; } = brokenRule;

    public string Details { get; } = brokenRule.Message;

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}