using Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Livestock Movements SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public interface IAnimalCohortServiceContract
{
    [OperationContract(Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Rpc)]
    Task<GetAnimalCohortResponse> GetAnimalCohortRequest(ServiceOptions ServiceOptions, AnimalCohortQuery AnimalCohortQuery);
}