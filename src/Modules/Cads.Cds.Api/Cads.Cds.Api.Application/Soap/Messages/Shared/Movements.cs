using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.Shared;

public class Movements
{
    [XmlElement("Movement", Namespace = Namespaces.CattleAnimal)]
    public List<Movement> Movement { get; set; } = [];
}

public class Movement
{
    [XmlElement("LivestockMovementStatus", Namespace = Namespaces.AnimalTypes)]
    public string LivestockMovementStatus { get; set; } = string.Empty;

    [XmlElement("MovementDateOn", Namespace = Namespaces.AnimalTypes)]
    public string MovementDateOn { get; set; } = string.Empty;

    [XmlElement("ReportRcvdDateTimeOn", Namespace = Namespaces.AnimalTypes)]
    public string ReportRcvdDateTimeOn { get; set; } = string.Empty;

    [XmlElement("OnFeature", Namespace = Namespaces.AnimalTypes)]
    public OnFeature OnFeature { get; set; } = new();

    [XmlElement("CTSMovementType", Namespace = Namespaces.CattleAnimalTypes)]
    public string CTSMovementType { get; set; } = string.Empty;

    [XmlElement("CTSMovementTypeDesc", Namespace = Namespaces.CattleAnimalTypes)]
    public string CTSMovementTypeDesc { get; set; } = string.Empty;

    [XmlElement("MovementDirection", Namespace = Namespaces.CattleAnimalTypes)]
    public string MovementDirection { get; set; } = string.Empty;

    [XmlElement("LocationType", Namespace = Namespaces.CattleAnimalTypes)]
    public string LocationType { get; set; } = string.Empty;
}

public class OnFeature
{
    [XmlElement("FeaturePK", Namespace = Namespaces.LocationTypes)]
    public string FeaturePK { get; set; } = string.Empty;

    [XmlElement("FeatureDetails", Namespace = Namespaces.LocationTypes)]
    public FeatureDetails FeatureDetails { get; set; } = new();

    [XmlElement("AltFeatureIdentities", Namespace = Namespaces.LocationTypes)]
    public AltFeatureIdentities AltFeatureIdentities { get; set; } = new();
}

public class FeatureDetails
{
    [XmlElement("FeatureType", Namespace = Namespaces.LocationTypes)]
    public RefDataSetCode FeatureType { get; set; } = new();

    [XmlElement("FeatureName", Namespace = Namespaces.LocationTypes)]
    public string FeatureName { get; set; } = string.Empty;
}

public class AltFeatureIdentities
{
    [XmlElement("AltFeatureIdentity", Namespace = Namespaces.LocationTypes)]
    public List<AltFeatureIdentity> AltFeatureIdentity { get; set; } = [];
}

public class AltFeatureIdentity
{
    [XmlElement("AltFeatureIdentityPK", Namespace = Namespaces.LocationTypes)]
    public string AltFeatureIdentityPK { get; set; } = string.Empty;

    [XmlElement("AltFeatureIdentityType", Namespace = Namespaces.LocationTypes)]
    public RefDataSetCode AltFeatureIdentityType { get; set; } = new();

    [XmlElement("AltFeatureIdentityValue", Namespace = Namespaces.LocationTypes)]
    public string AltFeatureIdentityValue { get; set; } = string.Empty;

    [XmlElement("AltFeatureIdFromDate", Namespace = Namespaces.LocationTypes)]
    public string AltFeatureIdFromDate { get; set; } = string.Empty;
}