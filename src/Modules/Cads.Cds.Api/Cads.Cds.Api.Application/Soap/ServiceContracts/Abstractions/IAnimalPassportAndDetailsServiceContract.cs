using Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

// <summary>
/// Service contract for Animal passport and details SOAP operations
/// </summary>
[ServiceContract(Namespace = Namespaces.AnimalPassport)]
public interface IAnimalPassportAndDetailsServiceContract
{
    [OperationContract(Name = "GetAnimalPassportAndDetails", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat]
    Task<GetAnimalPassportAndDetailsResponse> GetAnimalPassportAndDetails(GetAnimalPassportAndDetailsRequest request);
}