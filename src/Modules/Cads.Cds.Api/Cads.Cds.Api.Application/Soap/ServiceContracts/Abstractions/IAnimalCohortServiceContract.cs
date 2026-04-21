using Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Livestock Movements SOAP operations
/// </summary>
[ServiceContract(Namespace = Namespaces.LivestockMovements)]
public interface IAnimalCohortServiceContract
{
    [OperationContract(Name = "GetAnimalCohort", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat]
    GetAnimalCohortResponse GetAnimalCohort(GetAnimalCohortRequest request);
}