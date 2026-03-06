using Cads.Cds.Api.Application.Soap.Messages.AnimalDetails;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts.Abstractions;

/// <summary>
/// Service contract for Animal details SOAP operations
/// </summary>
[ServiceContract(Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
public interface IAnimalDetailsServiceContract
{
    [OperationContract(Name = "GetAnimalDetailsRequest", Action = "*", ReplyAction = "*")]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document)]
    Task<GetAnimalDetailsResponse> GetAnimalDetailsRequest(AnimalsIds AnimalsIds);
}