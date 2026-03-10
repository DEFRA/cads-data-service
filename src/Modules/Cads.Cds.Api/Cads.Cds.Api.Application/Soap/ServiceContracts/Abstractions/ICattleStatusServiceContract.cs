using Cads.Cds.Api.Application.Soap.Messages.CattleStatus;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Cattle Status SOAP operations
/// </summary>
[ServiceContract(Namespace = Namespaces.CattleHolding)]
public interface ICattleStatusServiceContract
{
    [OperationContract(Name = "GetCattleStatus", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat]
    Task<GetCattleStatusResponse> GetCattleStatus(GetCattleStatusRequest request);
}