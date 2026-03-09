using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.LivestockMovements;

/// <summary>
/// Response model for GetCattleStatus SOAP operation
/// </summary>
[XmlRoot("GetLivestockMovementsResponse", Namespace = Namespaces.LivestockMovements)]
[MessageContract(WrapperName = "GetLivestockMovementsResponse", WrapperNamespace = Namespaces.LivestockMovements, IsWrapped = true)]
public class GetLivestockMovementsResponse
{
    [XmlElement("TraceIdentifier", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 0)]
    public TraceIdentifier TraceIdentifier { get; set; } = new();

    [XmlElement("TraceParameter", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 1)]
    public TraceParameter TraceParameter { get; set; } = new();

    [XmlElement("SpeciesList", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 2)]
    public SpeciesList SpeciesList { get; set; } = new();
}

public class TraceParameter
{
    [XmlElement("TraceType", Namespace = "")]
    public string TraceType { get; set; } = string.Empty;

    [XmlElement("WindowStartDate", Namespace = "")]
    public string WindowStartDate { get; set; } = string.Empty;

    [XmlElement("WindowEndDate", Namespace = "")]
    public string WindowEndDate { get; set; } = string.Empty;
}

public class SpeciesList
{
    [XmlElement("Species", Namespace = "")]
    public List<Species> Species { get; set; } = [];
}

public class Species
{
    [XmlElement("SpeciesCode", Namespace = "")]
    public string SpeciesCode { get; set; } = string.Empty;

    [XmlElement("Movements", Namespace = "")]
    public Movements Movements { get; set; } = new();
}

public class Movements
{
    [XmlElement(ElementName = "Movement", Namespace = "")]
    public List<Movement> Movement { get; set; } = [];
}

public class Movement
{
    [XmlElement(ElementName = "AnimalIdentifier", Namespace = "")]
    public string AnimalIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "AnimalIdentifierType", Namespace = "")]
    public string AnimalIdentifierType { get; set; } = string.Empty;

    [XmlElement(ElementName = "DeathIndicator", Namespace = "")]
    public string DeathIndicator { get; set; } = string.Empty;

    [XmlElement(ElementName = "DateOfBirth", Namespace = "")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement(ElementName = "Gender", Namespace = "")]
    public string Gender { get; set; } = string.Empty;

    [XmlElement(ElementName = "BreedCode", Namespace = "")]
    public string BreedCode { get; set; } = string.Empty;

    [XmlElement(ElementName = "OffLocationIdentifier", Namespace = "")]
    public string OffLocationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "OffLocationIdentifierType", Namespace = "")]
    public string OffLocationIdentifierType { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnLocationIdentifier", Namespace = "")]
    public string OnLocationIdentifier { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnLocationIdentifierType", Namespace = "")]
    public string OnLocationIdentifierType { get; set; } = string.Empty;

    [XmlElement(ElementName = "MovementDateOff", Namespace = "")]
    public string MovementDateOff { get; set; } = string.Empty;

    [XmlElement(ElementName = "MovementDateOn", Namespace = "")]
    public string MovementDateOn { get; set; } = string.Empty;

    [XmlElement(ElementName = "SystemProvidingData", Namespace = "")]
    public string SystemProvidingData { get; set; } = string.Empty;

    [XmlElement(ElementName = "OffMovementRecordType", Namespace = "")]
    public string OffMovementRecordType { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnMovementRecordType", Namespace = "")]
    public string OnMovementRecordType { get; set; } = string.Empty;
}