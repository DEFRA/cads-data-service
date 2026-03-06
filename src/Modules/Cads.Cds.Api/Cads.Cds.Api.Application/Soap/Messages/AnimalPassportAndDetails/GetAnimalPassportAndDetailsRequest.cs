using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;

/// <summary>
/// Request model for GetAnimalPassportAndDetails SOAP operation
/// </summary>
[XmlRoot(ElementName = "GetAnimalPassportAndDetailsRequest", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
public class GetAnimalPassportAndDetailsRequest
{
    [XmlElement(ElementName = "AnimalsIds", Namespace = "")]
    public AnimalsIds? AnimalsIds { get; set; }
}

public class AnimalsIds
{
    [XmlElement("Eartag", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
    public List<string> Eartag { get; set; } = new();
}