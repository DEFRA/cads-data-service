using System.Net;

namespace Cads.Cds.BuildingBlocks.Core.Domain.BusinessRules;

public interface IBusinessRule
{
    HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    bool IsBroken();
    string Message { get; }
}