using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;

[XmlRoot(ElementName = "GetAnimalPassportAndDetailsResponse", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
public class GetAnimalPassportAndDetailsResponse
{
    [XmlElement(ElementName = "SearchResults", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
    public SearchResults? SearchResults { get; set; }
}

public class SearchResults
{
    [XmlElement(ElementName = "EartagResult", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
    public List<EartagResult> EartagResults { get; set; } = new();
}

public class EartagResult
{
    [XmlElement(ElementName = "DetailsFound", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
    public DetailsFound? DetailsFound { get; set; }

    [XmlElement(ElementName = "Eartag", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string Eartag { get; set; } = string.Empty;
}

public class DetailsFound
{
    [XmlElement(ElementName = "AnimalRecord", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
    public AnimalRecord? AnimalRecord { get; set; }

    [XmlElement(ElementName = "AnimalPassport", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
    public AnimalPassport? AnimalPassport { get; set; }

    [XmlElement(ElementName = "Movements", Namespace = "http://services.defra.gov.uk/ahw/animalpassport")]
    public Movements? Movements { get; set; }
}

public class AnimalRecord
{
    [XmlElement(ElementName = "AnimalPk", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public long AnimalPk { get; set; }

    [XmlElement(ElementName = "AnimalDetails", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public AnimalDetails? AnimalDetails { get; set; }

    [XmlElement(ElementName = "LivestockType", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public CodeType? LivestockType { get; set; }

    [XmlElement(ElementName = "IndividualAnimalReference", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public string IndividualAnimalReference { get; set; } = string.Empty;

    [XmlElement(ElementName = "DateOfBirth", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement(ElementName = "IndvdlyRegstAnimalStatus", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public string IndvdlyRegstAnimalStatus { get; set; } = string.Empty;

    [XmlElement(ElementName = "PassportVersion", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public int PassportVersion { get; set; }

    [XmlElement(ElementName = "Sire", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string Sire { get; set; } = string.Empty;

    [XmlElement(ElementName = "Dam", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string Dam { get; set; } = string.Empty;

    [XmlElement(ElementName = "RefusedPassport", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public bool RefusedPassport { get; set; }

    [XmlElement(ElementName = "Holdings", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public Holdings? Holdings { get; set; }
}

public class AnimalDetails
{
    [XmlElement(ElementName = "AnimalSpecies", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public CodeType? AnimalSpecies { get; set; }

    [XmlElement(ElementName = "Breed", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public CodeType? Breed { get; set; }

    [XmlElement(ElementName = "AnimalType", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public CodeType? AnimalType { get; set; }

    [XmlElement(ElementName = "Gender", Namespace = "http://types.defra.gov.uk/ahw/asset")]
    public string Gender { get; set; } = string.Empty;
}

public class CodeType
{
    [XmlElement(ElementName = "Code", Namespace = "http://types.defra.gov.uk/ahw/common/referencedatasets")]
    public string Code { get; set; } = string.Empty;
}

public class Holdings
{
    [XmlElement(ElementName = "AnimalOnFarm", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public List<AnimalOnFarm> AnimalOnFarm { get; set; } = new();
}

public class AnimalOnFarm
{
    [XmlElement(ElementName = "HoldingId", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string HoldingId { get; set; } = string.Empty;

    [XmlElement(ElementName = "CurrentlyOnLocation", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public bool CurrentlyOnLocation { get; set; }
}

public class AnimalPassport
{
    [XmlElement(ElementName = "BirthDam", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public string BirthDam { get; set; } = string.Empty;

    [XmlElement(ElementName = "PassportStatusCode", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public int PassportStatusCode { get; set; }

    [XmlElement(ElementName = "DateOfRegistration", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public string DateOfRegistration { get; set; } = string.Empty;

    [XmlElement(ElementName = "PassportVersion", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public int PassportVersion { get; set; }

    [XmlElement(ElementName = "RefusedPassport", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public bool RefusedPassport { get; set; }

    [XmlElement(ElementName = "DamIsFemale", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public bool DamIsFemale { get; set; }

    [XmlElement(ElementName = "DamAliveAtBirth", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public bool DamAliveAtBirth { get; set; }

    [XmlElement(ElementName = "DamMinimumAgeAtBirth", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public bool DamMinimumAgeAtBirth { get; set; }

    [XmlElement(ElementName = "DamOnLocationAtBirth", Namespace = "http://types.defra.gov.uk/ahw/animalpassport")]
    public bool DamOnLocationAtBirth { get; set; }
}

public class Movements
{
    [XmlElement(ElementName = "Movement", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/animal")]
    public List<Movement> Movement { get; set; } = new();
}

public class Movement
{
    [XmlElement(ElementName = "LivestockMovementStatus", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public string LivestockMovementStatus { get; set; } = string.Empty;

    [XmlElement(ElementName = "MovementDateOn", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public string MovementDateOn { get; set; } = string.Empty;

    [XmlElement(ElementName = "ReportRcvdDateTimeOn", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public string ReportRcvdDateTimeOn { get; set; } = string.Empty;

    [XmlElement(ElementName = "OnFeature", Namespace = "http://types.defra.gov.uk/ahw/animal")]
    public OnFeature? OnFeature { get; set; }

    [XmlElement(ElementName = "CTSMovementType", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public int CTSMovementType { get; set; }

    [XmlElement(ElementName = "CTSMovementTypeDesc", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string CTSMovementTypeDesc { get; set; } = string.Empty;

    [XmlElement(ElementName = "MovementDirection", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string MovementDirection { get; set; } = string.Empty;

    [XmlElement(ElementName = "LocationType", Namespace = "http://types.defra.gov.uk/ahw/tracing/cattle/animal")]
    public string LocationType { get; set; } = string.Empty;
}

public class OnFeature
{
    [XmlElement(ElementName = "FeaturePK", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public long FeaturePK { get; set; }

    [XmlElement(ElementName = "FeatureDetails", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public FeatureDetails? FeatureDetails { get; set; }

    [XmlElement(ElementName = "AltFeatureIdentities", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public AltFeatureIdentities? AltFeatureIdentities { get; set; }
}

public class FeatureDetails
{
    [XmlElement(ElementName = "FeatureType", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public CodeType? FeatureType { get; set; }

    [XmlElement(ElementName = "FeatureName", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public string FeatureName { get; set; } = string.Empty;
}

public class AltFeatureIdentities
{
    [XmlElement(ElementName = "AltFeatureIdentity", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public List<AltFeatureIdentity> AltFeatureIdentity { get; set; } = new();
}

public class AltFeatureIdentity
{
    [XmlElement(ElementName = "AltFeatureIdentityPK", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public long AltFeatureIdentityPK { get; set; }

    [XmlElement(ElementName = "AltFeatureIdentityType", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public CodeType? AltFeatureIdentityType { get; set; }

    [XmlElement(ElementName = "AltFeatureIdentityValue", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public string AltFeatureIdentityValue { get; set; } = string.Empty;

    [XmlElement(ElementName = "AltFeatureIdFromDate", Namespace = "http://types.defra.gov.uk/ahw/location")]
    public string AltFeatureIdFromDate { get; set; } = string.Empty;
}