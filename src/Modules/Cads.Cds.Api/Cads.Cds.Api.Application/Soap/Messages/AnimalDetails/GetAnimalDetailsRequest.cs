using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalDetails;

/// <summary>
/// Request model for GetAnimalDetails SOAP operation
/// </summary>
[XmlRoot(ElementName = "GetAnimalDetailsRequest", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
public class GetAnimalDetailsRequest
{
    [XmlAttribute(AttributeName = "IncludeRecentMovements")]
    public bool IncludeRecentMovements { get; set; }

    [XmlAttribute(AttributeName = "IncludeFeedback")]
    public bool IncludeFeedback { get; set; }

    [XmlElement(ElementName = "AnimalsIds", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
    public AnimalsIds? AnimalsIds { get; set; }
}

public class AnimalsIds
{
    [XmlElement(ElementName = "Eartag", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
    public List<string> Eartags { get; set; } = new();
}