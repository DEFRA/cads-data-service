using Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

// <summary>
/// Service contract for Animal passport and details SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
public interface IAnimalPassportAndDetailsServiceContract
{
    [OperationContract(Name = "GetAnimalPassportAndDetailsRequest", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document)]
    Task<GetAnimalPassportAndDetailsResponse> GetAnimalPassportAndDetailsRequest(AnimalsIds AnimalsIds);
}