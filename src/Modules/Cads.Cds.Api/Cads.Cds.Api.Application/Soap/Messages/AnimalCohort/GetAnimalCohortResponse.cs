using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;

/// <summary>
/// Response model for GetAnimalCohort SOAP operation
/// </summary>
[XmlRoot("GetAnimalCohortResponse", Namespace = Namespaces.LivestockMovements)]
[MessageContract(WrapperName = "GetAnimalCohortResponse", WrapperNamespace = Namespaces.LivestockMovements, IsWrapped = true)]
public class GetAnimalCohortResponse
{
    [XmlElement("CohortAnimals", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 0)]
    public CohortAnimals CohortAnimals { get; set; } = new();

    [XmlElement("TraceIdentifier", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 1)]
    public TraceIdentifier TraceIdentifier { get; set; } = new();
}

public class CohortAnimals
{
    [XmlElement("CohortAnimal")]
    public List<CohortAnimal> CohortAnimal { get; set; } = [];
}

public class CohortAnimal
{
    [XmlElement("CohortType")]
    public string CohortType { get; set; } = string.Empty;

    [XmlElement("SpeciesCodeAndAnimalIdentifiers")]
    public SpeciesCodeAndAnimalIdentifiers SpeciesCodeAndAnimalIdentifiers { get; set; } = new();

    [XmlElement("DateOfBirth")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement("Gender")]
    public RefDataSetCode Gender { get; set; } = new();

    [XmlElement("TargetLocation")]
    public CohortLocation TargetLocation { get; set; } = new();

    [XmlElement("LastKnownLocation")]
    public CohortLocation LastKnownLocation { get; set; } = new();

    [XmlElement("BreedCode")]
    public RefDataSetCode BreedCode { get; set; } = new();
}

public class SpeciesCodeAndAnimalIdentifiers
{
    [XmlElement("AnimalSpecies")]
    public RefDataSetCode AnimalSpecies { get; set; } = new();

    [XmlElement("AnimalIdentifiers")]
    public AnimalIdentifiers AnimalIdentifiers { get; set; } = new();
}

public class CohortLocation
{
    [XmlElement("MovementOnDate")]
    public string MovementOnDate { get; set; } = string.Empty;

    [XmlElement("Location")]
    public LocationDetail Location { get; set; } = new();
}

public class LocationDetail
{
    [XmlElement("PrimaryLocationIdentifiersAndTypes")]
    public PrimaryLocationIdentifiersAndTypes PrimaryLocationIdentifiersAndTypes { get; set; } = new();

    [XmlElement("LocationType")]
    public RefDataSetCode LocationSetCode { get; set; } = new();
}

public class PrimaryLocationIdentifiersAndTypes
{
    [XmlElement("LocationIdentifierAndType")]
    public List<LocationIdentifierAndType> LocationIdentifierAndType { get; set; } = [];
}

public class LocationIdentifierAndType
{
    [XmlElement("LocationIdentifier")]
    public string LocationIdentifier { get; set; } = string.Empty;

    [XmlElement("LocationIdentifierType")]
    public RefDataSetCode LocationIdentifierSetCode { get; set; } = new();
}