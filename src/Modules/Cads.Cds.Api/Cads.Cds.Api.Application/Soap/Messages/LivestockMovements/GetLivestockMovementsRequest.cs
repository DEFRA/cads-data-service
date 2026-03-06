using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;

namespace Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;

/// <summary>
/// Request model for GetLivestockMovements SOAP operation
/// </summary>
[XmlRoot("GetLivestockMovementsRequest", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class GetLivestockMovementsRequest
{
    [XmlElement(ElementName = "MovementQuery", Namespace = "")]
    public MovementQuery? MovementQuery { get; set; }

    [XmlElement("ServiceOptions", Namespace = "")]
    public ServiceOptions? ServiceOptions { get; set; }
}

public class MovementQuery
{
    [XmlElement("TraceType", Namespace = "")]
    public string TraceType { get; set; } = string.Empty;

    [XmlElement("WindowStartDate", Namespace = "")]
    public string WindowStartDate { get; set; } = string.Empty;

    [XmlElement("WindowEndDate", Namespace = "")]
    public string WindowEndDate { get; set; } = string.Empty;

    [XmlElement("TraceIdentifier", Namespace = "")]
    public TraceIdentifier? TraceIdentifier { get; set; }

    [XmlElement("LocationsAndSpeciesCodes", Namespace = "")]
    public LocationsAndSpeciesCodes? LocationsAndSpeciesCodes { get; set; }

    [XmlElement("MaxiRowsToBeReturned", Namespace = "")]
    public int MaxiRowsToBeReturned { get; set; }
}

public class TraceIdentifier
{
    [XmlElement(ElementName = "TraceSpecificationIdentifier")]
    public string TraceSpecificationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "TraceIdentifier")]
    public string TraceIdentifierValue { get; set; } = string.Empty;
}

public class LocationsAndSpeciesCodes
{
    [XmlElement(ElementName = "LocationAndSpeciesCodes")]
    public List<LocationAndSpeciesCodes> LocationAndSpeciesCodesList { get; set; } = new();
}

public class LocationAndSpeciesCodes
{
    [XmlElement(ElementName = "LocationIdentifier")]
    public string LocationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "LocationIdentifierType")]
    public LocationIdentifierType? LocationIdentifierType { get; set; }

    [XmlElement(ElementName = "SpeciesCodes")]
    public SpeciesCodes? SpeciesCodes { get; set; }
}

public class LocationIdentifierType
{
    [XmlAttribute(AttributeName = "RefDataSetName")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement(ElementName = "Code", Namespace = "http://types.defra.gov.uk/ahw/common/referencedatasets")]
    public string Code { get; set; } = string.Empty;
}

public class SpeciesCodes
{
    [XmlElement(ElementName = "AnimalSpecies")]
    public List<AnimalSpecies> AnimalSpecies { get; set; } = new();
}

public class AnimalSpecies
{
    [XmlAttribute(AttributeName = "RefDataSetName")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement(ElementName = "Code", Namespace = "http://types.defra.gov.uk/ahw/common/referencedatasets")]
    public string Code { get; set; } = string.Empty;
}