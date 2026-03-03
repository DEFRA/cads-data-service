using Cads.Cds.Api.Application.Soap.Messages;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

/// <summary>
/// Service contract for Livestock Movements SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public interface ILivestockMovementsService
{
    [OperationContract(Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Rpc)]
    Task<GetAnimalCohortResponse> GetAnimalCohortRequest(ServiceOptions ServiceOptions, AnimalCohortQuery AnimalCohortQuery);
}