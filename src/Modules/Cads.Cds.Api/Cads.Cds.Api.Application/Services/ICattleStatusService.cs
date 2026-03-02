using Cads.Cds.Api.Application.Soap;
using CoreWCF;

namespace Cads.Cds.Api.Application.Services;

/// <summary>
/// Service contract for Cattle Status SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/holding")]
public interface ICattleStatusService
{
    [OperationContract(Name = "GetCattleStatusRequest", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document)]
    Task<GetCattleStatusResponse> GetCattleStatusRequest(string HoldingId);
}