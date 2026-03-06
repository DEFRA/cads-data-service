using Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Livestock movement SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public interface ILivestockMovementsServiceContract
{
    [OperationContract(Name = "GetLivestockMovementsRequest", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Rpc)]
    Task<GetLivestockMovementsResponse> GetLivestockMovementsRequest(MovementQuery MovementQuery, ServiceOptions ServiceOptions);

}