using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.AnimalCohort;

/// <summary>
/// Request model for GetAnimalCohort SOAP operation
/// </summary>
[XmlRoot("GetAnimalCohortRequest", Namespace = Namespaces.LivestockMovements)]
[MessageContract(WrapperName = "GetAnimalCohortRequest", WrapperNamespace = Namespaces.LivestockMovements, IsWrapped = true)]
public class GetAnimalCohortRequest
{
    [XmlElement("ServiceOptions", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 0)]
    public ServiceOptions ServiceOptions { get; set; } = new();

    [XmlElement("AnimalCohortQuery", Namespace = "")]
    [MessageBodyMember(Namespace = "", Order = 1)]
    public AnimalCohortQuery AnimalCohortQuery { get; set; } = new();
}

public class AnimalCohortQuery
{
    [XmlElement("TraceIdentifier")]
    public TraceIdentifier TraceIdentifier { get; set; } = new();

    [XmlElement("Locations")]
    public Locations Locations { get; set; } = new();

    [XmlElement("Gender")]
    public RefDataSetCode Gender { get; set; } = new();

    [XmlElement("SpeciesCodesAndAnimals")]
    public SpeciesCodesAndAnimals SpeciesCodesAndAnimals { get; set; } = new();

    [XmlElement("DateOfBirth")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement("BirthLocationIdentifier")]
    public string BirthLocationIdentifier { get; set; } = string.Empty;

    [XmlElement("BirthLocationIdentifierType")]
    public RefDataSetCode BirthLocationIdentifierSetCode { get; set; } = new();
}

public class Locations
{
    [XmlElement("Location")]
    public List<Location> Location { get; set; } = [];
}

public class Location
{
    [XmlElement("WindowEndDate")]
    public string WindowEndDate { get; set; } = string.Empty;

    [XmlElement("WindowStartDate")]
    public string WindowStartDate { get; set; } = string.Empty;

    [XmlElement("TargetLocationIdentifier")]
    public string TargetLocationIdentifier { get; set; } = string.Empty;

    [XmlElement("TargetLocationIdentifierType")]
    public RefDataSetCode TargetLocationIdentifierSetCode { get; set; } = new();
}


public class SpeciesCodesAndAnimals
{
    [XmlElement("SpeciesCodeAndAnimals")]
    public List<SpeciesCodeAndAnimals> SpeciesCodeAndAnimalsList { get; set; } = [];
}

public class SpeciesCodeAndAnimals
{
    [XmlElement("AnimalSpecies")]
    public RefDataSetCode AnimalSpecies { get; set; } = new();

    [XmlElement("AnimalIdentifiers")]
    public AnimalIdentifiers AnimalIdentifiers { get; set; } = new();
}

public class AnimalIdentifiers
{
    [XmlElement("AnimalIdentifier")]
    public List<AnimalIdentifier> AnimalIdentifier { get; set; } = [];
}

public class AnimalIdentifier
{
    [XmlElement("AnimalIdentifier")]
    public string AnimalIdentifierValue { get; set; } = string.Empty;

    [XmlElement("AnimalIdentifierType")]
    public RefDataSetCode AnimalIdentifierSetCode { get; set; } = new();
}