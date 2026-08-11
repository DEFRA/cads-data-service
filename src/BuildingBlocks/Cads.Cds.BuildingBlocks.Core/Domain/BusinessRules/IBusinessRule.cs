namespace Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}