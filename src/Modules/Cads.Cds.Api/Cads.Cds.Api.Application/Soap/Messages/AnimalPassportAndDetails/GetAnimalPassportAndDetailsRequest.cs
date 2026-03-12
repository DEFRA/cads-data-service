using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;

/// <summary>
/// Request model for GetAnimalPassportAndDetails SOAP operation
/// </summary>
[XmlRoot("GetAnimalPassportAndDetailsRequest", Namespace = Namespaces.AnimalPassport)]
[MessageContract(WrapperName = "GetAnimalPassportAndDetailsRequest", WrapperNamespace = Namespaces.AnimalPassport, IsWrapped = true)]
public class GetAnimalPassportAndDetailsRequest
{
    [MessageBodyMember(Name = "AnimalsIds", Namespace = Namespaces.AnimalPassport, Order = 0)]
    [XmlElement("AnimalsIds", Namespace = Namespaces.AnimalPassport)]
    public AnimalsIds AnimalsIds { get; set; } = new();
}

public class AnimalsIds
{
    [XmlElement("Eartag", Namespace = Namespaces.CattleAnimal)]
    public List<string> Eartag { get; set; } = [];
}