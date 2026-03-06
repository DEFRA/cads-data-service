using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;

/// <summary>
/// Response model for GetCattleStatus SOAP operation
/// </summary>
[XmlRoot("GetLivestockMovementsResponse", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class GetLivestockMovementsResponse
{
    [XmlElement("TraceIdentifier", Namespace = "")]
    public TraceIdentifier? TraceIdentifier { get; set; }

    [XmlElement("TraceParameter", Namespace = "")]
    public TraceParameter? TraceParameter { get; set; }

    [XmlElement("SpeciesList", Namespace = "")]
    public SpeciesList? SpeciesList { get; set; }
}

public class TraceParameter
{
    [XmlElement("TraceType", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string TraceType { get; set; } = string.Empty;

    [XmlElement("WindowStartDate", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string WindowStartDate { get; set; } = string.Empty;

    [XmlElement("WindowEndDate", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string WindowEndDate { get; set; } = string.Empty;
}

public class SpeciesList
{
    [XmlElement("Species", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public List<Species> Species { get; set; } = new();
}

public class Species
{
    [XmlElement("SpeciesCode", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string SpeciesCode { get; set; } = string.Empty;

    [XmlElement("Movements", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public Movements? Movements { get; set; }
}

public class Movements
{
    [XmlElement(ElementName = "Movement", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public List<Movement> Movement { get; set; } = new();
}

public class Movement
{
    [XmlElement(ElementName = "AnimalIdentifier", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string AnimalIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "AnimalIdentifierType", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string AnimalIdentifierType { get; set; } = string.Empty;

    [XmlElement(ElementName = "DeathIndicator", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string DeathIndicator { get; set; } = string.Empty;

    [XmlElement(ElementName = "DateOfBirth", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement(ElementName = "Gender", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string Gender { get; set; } = string.Empty;

    [XmlElement(ElementName = "BreedCode", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string BreedCode { get; set; } = string.Empty;

    [XmlElement(ElementName = "OffLocationIdentifier", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string OffLocationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "OffLocationIdentifierType", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string OffLocationIdentifierType { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnLocationIdentifier", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string OnLocationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnLocationIdentifierType", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string OnLocationIdentifierType { get; set; } = string.Empty;

    [XmlElement(ElementName = "MovementDateOff", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string MovementDateOff { get; set; } = string.Empty;

    [XmlElement(ElementName = "MovementDateOn", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string MovementDateOn { get; set; } = string.Empty;

    [XmlElement(ElementName = "SystemProvidingData", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string SystemProvidingData { get; set; } = string.Empty;

    [XmlElement(ElementName = "OffMovementRecordType", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string OffMovementRecordType { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnMovementRecordType", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string OnMovementRecordType { get; set; } = string.Empty;
}