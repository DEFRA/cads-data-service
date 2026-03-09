using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalPassportAndDetails;

[XmlRoot(ElementName = "GetAnimalPassportAndDetailsResponse", Namespace = Namespaces.AnimalPassport)]
[MessageContract(WrapperName = "GetAnimalPassportAndDetailsResponse", WrapperNamespace = Namespaces.AnimalPassport, IsWrapped = true)]
public class GetAnimalPassportAndDetailsResponse
{
    [XmlElement("SearchResults", Namespace = Namespaces.AnimalPassport)]
    [MessageBodyMember(Namespace = Namespaces.AnimalPassport, Order = 0)]
    public SearchResults? SearchResults { get; set; }
}

public class SearchResults
{
    [XmlElement("EartagResult", Namespace = Namespaces.AnimalPassport)]
    public List<EartagResult> EartagResults { get; set; } = [];
}

public class EartagResult
{
    [XmlElement("DetailsFound", Namespace = Namespaces.AnimalPassport)]
    public DetailsFound? DetailsFound { get; set; }

    [XmlElement("Eartag", Namespace = Namespaces.CattleAnimal)]
    public string Eartag { get; set; } = string.Empty;
}

public class DetailsFound
{
    [XmlElement("AnimalRecord", Namespace = Namespaces.AnimalPassport)]
    public AnimalRecord? AnimalRecord { get; set; }

    [XmlElement("AnimalPassport", Namespace = Namespaces.AnimalPassport)]
    public AnimalPassport? AnimalPassport { get; set; }

    [XmlElement("Movements", Namespace = Namespaces.AnimalPassport)]
    public Movements? Movements { get; set; }
}

public class AnimalRecord
{
    [XmlElement("AnimalPk", Namespace = Namespaces.AssetTypes)]
    public long AnimalPk { get; set; }

    [XmlElement("AnimalDetails", Namespace = Namespaces.AssetTypes)]
    public AnimalDetails AnimalDetails { get; set; } = new();

    [XmlElement("LivestockType", Namespace = Namespaces.AssetTypes)]
    public RefDataSetCode LivestockType { get; set; } = new();

    [XmlElement("IndividualAnimalReference", Namespace = Namespaces.AnimalTypes)]
    public string IndividualAnimalReference { get; set; } = string.Empty;

    [XmlElement("DateOfBirth", Namespace = Namespaces.AnimalTypes)]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement("IndvdlyRegstAnimalStatus", Namespace = Namespaces.AnimalTypes)]
    public string IndvdlyRegstAnimalStatus { get; set; } = string.Empty;

    [XmlElement("PassportVersion", Namespace = Namespaces.CattleAnimalTypes)]
    public int PassportVersion { get; set; }

    [XmlElement("Sire", Namespace = Namespaces.CattleAnimalTypes)]
    public string Sire { get; set; } = string.Empty;

    [XmlElement("Dam", Namespace = Namespaces.CattleAnimalTypes)]
    public string Dam { get; set; } = string.Empty;

    [XmlElement("RefusedPassport", Namespace = Namespaces.CattleAnimalTypes)]
    public bool RefusedPassport { get; set; }

    [XmlElement("Holdings", Namespace = Namespaces.CattleAnimalTypes)]
    public Holdings Holdings { get; set; } = new();
}

public class AnimalDetails
{
    [XmlElement("AnimalSpecies", Namespace = Namespaces.AssetTypes)]
    public RefDataSetCode AnimalSpecies { get; set; } = new();

    [XmlElement("Breed", Namespace = Namespaces.AssetTypes)]
    public RefDataSetCode Breed { get; set; } = new();

    [XmlElement("AnimalType", Namespace = Namespaces.AssetTypes)]
    public RefDataSetCode AnimalType { get; set; } = new();

    [XmlElement("Gender", Namespace = Namespaces.AssetTypes)]
    public string Gender { get; set; } = string.Empty;
}

public class Holdings
{
    [XmlElement("AnimalOnFarm", Namespace = Namespaces.CattleAnimalTypes)]
    public List<AnimalOnFarm> AnimalOnFarm { get; set; } = [];
}

public class AnimalOnFarm
{
    [XmlElement("HoldingId", Namespace = Namespaces.CattleAnimalTypes)]
    public string HoldingId { get; set; } = string.Empty;

    [XmlElement("CurrentlyOnLocation", Namespace = Namespaces.CattleAnimalTypes)]
    public bool CurrentlyOnLocation { get; set; }
}

public class AnimalPassport
{
    [XmlElement("BirthDam", Namespace = Namespaces.AnimalPassportTypes)]
    public string BirthDam { get; set; } = string.Empty;

    [XmlElement("PassportStatusCode", Namespace = Namespaces.AnimalPassportTypes)]
    public int PassportStatusCode { get; set; }

    [XmlElement("DateOfRegistration", Namespace = Namespaces.AnimalPassportTypes)]
    public string DateOfRegistration { get; set; } = string.Empty;

    [XmlElement("PassportVersion", Namespace = Namespaces.AnimalPassportTypes)]
    public int PassportVersion { get; set; }

    [XmlElement("RefusedPassport", Namespace = Namespaces.AnimalPassportTypes)]
    public bool RefusedPassport { get; set; }

    [XmlElement("DamIsFemale", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamIsFemale { get; set; }

    [XmlElement("DamAliveAtBirth", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamAliveAtBirth { get; set; }

    [XmlElement("DamMinimumAgeAtBirth", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamMinimumAgeAtBirth { get; set; }

    [XmlElement("DamOnLocationAtBirth", Namespace = Namespaces.AnimalPassportTypes)]
    public bool DamOnLocationAtBirth { get; set; }
}

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
    public OnFeature? OnFeature { get; set; }

    [XmlElement("CTSMovementType", Namespace = Namespaces.CattleAnimalTypes)]
    public int CTSMovementType { get; set; }

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
    public long FeaturePK { get; set; }

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
    public long AltFeatureIdentityPK { get; set; }

    [XmlElement("AltFeatureIdentityType", Namespace = Namespaces.LocationTypes)]
    public RefDataSetCode AltFeatureIdentityType { get; set; } = new();

    [XmlElement("AltFeatureIdentityValue", Namespace = Namespaces.LocationTypes)]
    public string AltFeatureIdentityValue { get; set; } = string.Empty;

    [XmlElement("AltFeatureIdFromDate", Namespace = Namespaces.LocationTypes)]
    public string AltFeatureIdFromDate { get; set; } = string.Empty;
}