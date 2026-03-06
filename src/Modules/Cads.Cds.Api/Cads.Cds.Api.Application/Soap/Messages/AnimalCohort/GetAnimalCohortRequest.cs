using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;

/// <summary>
/// Request model for GetAnimalCohort SOAP operation
/// </summary>
[XmlRoot("GetAnimalCohortRequest", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class GetAnimalCohortRequest
{
    [XmlElement("ServiceOptions", Namespace = "")]
    public ServiceOptions? ServiceOptions { get; set; }

    [XmlElement("AnimalCohortQuery", Namespace = "")]
    public AnimalCohortQuery? AnimalCohortQuery { get; set; }
}

public class AnimalCohortQuery
{
    [XmlElement("TraceIdentifier", Namespace = "")]
    public TraceIdentifier? TraceIdentifier { get; set; }

    [XmlElement("Locations", Namespace = "")]
    public Locations? Locations { get; set; }

    [XmlElement("Gender", Namespace = "")]
    public ReferenceDataType? Gender { get; set; }

    [XmlElement("SpeciesCodesAndAnimals", Namespace = "")]
    public SpeciesCodesAndAnimals? SpeciesCodesAndAnimals { get; set; }

    [XmlElement(Namespace = "")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public string BirthLocationIdentifier { get; set; } = string.Empty;

    [XmlElement("BirthLocationIdentifierType", Namespace = "")]
    public ReferenceDataType? BirthLocationIdentifierType { get; set; }
}

public class TraceIdentifier
{
    [XmlElement(Namespace = "")]
    public string TraceSpecificationIdentifier { get; set; } = string.Empty;

    [XmlElement("TraceIdentifier", Namespace = "")]
    public string TraceIdentifierValue { get; set; } = string.Empty;
}

public class Locations
{
    [XmlElement("Location", Namespace = "")]
    public List<Location> Location { get; set; } = new();
}

public class Location
{
    [XmlElement(Namespace = "")]
    public string WindowEndDate { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public string WindowStartDate { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public string TargetLocationIdentifier { get; set; } = string.Empty;

    [XmlElement("TargetLocationIdentifierType", Namespace = "")]
    public ReferenceDataType? TargetLocationIdentifierType { get; set; }
}

public class ReferenceDataType
{
    [XmlAttribute(Namespace = "")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://types.defra.gov.uk/ahw/common/referencedatasets")]
    public string Code { get; set; } = string.Empty;
}

public class SpeciesCodesAndAnimals
{
    [XmlElement("SpeciesCodeAndAnimals", Namespace = "")]
    public List<SpeciesCodeAndAnimals> SpeciesCodeAndAnimalsList { get; set; } = new();
}

public class SpeciesCodeAndAnimals
{
    [XmlElement("AnimalSpecies", Namespace = "")]
    public ReferenceDataType? AnimalSpecies { get; set; }

    [XmlElement("AnimalIdentifiers", Namespace = "")]
    public AnimalIdentifiers? AnimalIdentifiers { get; set; }
}

public class AnimalIdentifiers
{
    [XmlElement("AnimalIdentifier", Namespace = "")]
    public List<AnimalIdentifier> AnimalIdentifier { get; set; } = new();
}

public class AnimalIdentifier
{
    [XmlElement("AnimalIdentifier", Namespace = "")]
    public string AnimalIdentifierValue { get; set; } = string.Empty;

    [XmlElement("AnimalIdentifierType", Namespace = "")]
    public ReferenceDataType? AnimalIdentifierType { get; set; }
}