using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;

/// <summary>
/// Request model for GetLivestockMovements SOAP operation
/// </summary>
[XmlRoot("GetLivestockMovementsRequest", Namespace = Namespaces.LivestockMovements)]
[MessageContract(WrapperName = "GetLivestockMovementsRequest", WrapperNamespace = Namespaces.LivestockMovements, IsWrapped = true)]
public class GetLivestockMovementsRequest
{
    [XmlElement("MovementQuery", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 0)]
    public MovementQuery? MovementQuery { get; set; }

    [XmlElement("ServiceOptions", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 1)]
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
    public TraceIdentifier TraceIdentifier { get; set; } = new();

    [XmlElement("LocationsAndSpeciesCodes", Namespace = "")]
    public LocationsAndSpeciesCodes LocationsAndSpeciesCodes { get; set; } = new();

    [XmlElement("MaxiRowsToBeReturned", Namespace = "")]
    public int MaxiRowsToBeReturned { get; set; }
}

public class LocationsAndSpeciesCodes
{
    [XmlElement(ElementName = "LocationAndSpeciesCodes", Namespace = "")]
    public List<LocationAndSpeciesCodes> LocationAndSpeciesCodesList { get; set; } = [];
}

public class LocationAndSpeciesCodes
{
    [XmlElement(ElementName = "LocationIdentifier")]
    public string LocationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "LocationIdentifierType")]
    public CommonRefDataSetCode LocationIdentifierType { get; set; } = new();

    [XmlElement(ElementName = "SpeciesCodes")]
    public SpeciesCodes SpeciesCodes { get; set; } = new();
}

public class SpeciesCodes
{
    [XmlElement(ElementName = "AnimalSpecies")]
    public List<CommonRefDataSetCode> AnimalSpecies { get; set; } = [];
}

public class CommonRefDataSetCode
{
    [XmlAttribute("RefDataSetName")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement("Code", Namespace = Namespaces.CommonReferenceDataSetTypes)]
    public string Code { get; set; } = string.Empty;
}