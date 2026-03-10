using Cads.Cds.Api.Application.Soap.Messages.AnimalDetails;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Animal details SOAP operations
/// </summary>
[ServiceContract(Namespace = Namespaces.CattleAnimal)]
public interface IAnimalDetailsServiceContract
{
    [OperationContract(Name = "GetAnimalDetails", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat]
    Task<GetAnimalDetailsResponse> GetAnimalDetails(GetAnimalDetailsRequest request);
}