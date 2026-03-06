using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;

/// <summary>
/// Response model for GetAnimalCohort SOAP operation
/// </summary>
[XmlRoot("GetAnimalCohortResponse", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class GetAnimalCohortResponse
{
    [XmlElement("CohortAnimals", Namespace = "")]
    public CohortAnimals? CohortAnimals { get; set; }

    [XmlElement("TraceIdentifier", Namespace = "")]
    public TraceIdentifier? TraceIdentifier { get; set; }
}

public class CohortAnimals
{
    [XmlElement("CohortAnimal", Namespace = "")]
    public List<CohortAnimal> CohortAnimal { get; set; } = new();
}

public class CohortAnimal
{
    [XmlElement(Namespace = "")]
    public string CohortType { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public SpeciesCodeAndAnimalIdentifiers? SpeciesCodeAndAnimalIdentifiers { get; set; }

    [XmlElement(Namespace = "")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public ReferenceDataType? Gender { get; set; }

    [XmlElement(Namespace = "")]
    public TargetLocation? TargetLocation { get; set; }

    [XmlElement(Namespace = "")]
    public TargetLocation? LastKnownLocation { get; set; }

    [XmlElement(Namespace = "")]
    public ReferenceDataType? BreedCode { get; set; }
}

public class SpeciesCodeAndAnimalIdentifiers
{
    [XmlElement("AnimalSpecies", Namespace = "")]
    public ReferenceDataType? AnimalSpecies { get; set; }

    [XmlElement("AnimalIdentifiers", Namespace = "")]
    public AnimalIdentifiers? AnimalIdentifiers { get; set; }
}

public class TargetLocation
{
    [XmlElement(Namespace = "")]
    public string MovementOnDate { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public PrimaryLocationIdentifiersAndTypes? Location { get; set; }
}

public class PrimaryLocationIdentifiersAndTypes
{
    [XmlElement(Namespace = "")]
    public LocationIdentifierAndType? LocationIdentifierAndType { get; set; }

    [XmlElement(Namespace = "")]
    public ReferenceDataType? LocationType { get; set; }
}

public class LocationIdentifierAndType
{
    [XmlElement(Namespace = "")]
    public string LocationIdentifier { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public ReferenceDataType? LocationIdentifierType { get; set; }
}