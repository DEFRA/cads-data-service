using Cads.Cds.Api.Application.Soap.Messages.CattleStatus;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Cattle Status SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/holding")]
public interface ICattleStatusServiceContract
{
    [OperationContract(Name = "GetCattleStatusRequest", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document)]
    Task<GetCattleStatusResponse> GetCattleStatusRequest(string HoldingId);
}