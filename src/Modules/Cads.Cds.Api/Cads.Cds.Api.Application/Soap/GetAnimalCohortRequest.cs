using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Serialization;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap;

/// <summary>
/// Request model for GetAnimalCohort SOAP operation
/// </summary>
[MessageContract(WrapperName = "GetAnimalCohortRequest", WrapperNamespace = "http://services.defra.gov.uk/ahw/livestockmovements", IsWrapped = true)]
public class GetAnimalCohortRequest
{
    [MessageBodyMember(Namespace = "")]
    public ServiceOptions? ServiceOptions { get; set; }

    [MessageBodyMember(Namespace = "")]
    public AnimalCohortQuery? AnimalCohortQuery { get; set; }
}

[DataContract(Namespace = "")]
public class ServiceOptions
{
    [DataMember]
    public string DestinationDataBaseName { get; set; } = string.Empty;

    [DataMember]
    public string DestinationStoredProcedure { get; set; } = string.Empty;
}

[XmlRoot("AnimalCohortQuery", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class AnimalCohortQuery
{
    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public TraceIdentifier? TraceIdentifier { get; set; }

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public Locations? Locations { get; set; }

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public ReferenceDataType? Gender { get; set; }

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public SpeciesCodesAndAnimals? SpeciesCodesAndAnimals { get; set; }

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string DateOfBirth { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string BirthLocationIdentifier { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public ReferenceDataType? BirthLocationIdentifierType { get; set; }
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class TraceIdentifier
{
    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string TraceSpecificationIdentifier { get; set; } = string.Empty;

    [XmlElement("TraceIdentifier", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string TraceIdentifierValue { get; set; } = string.Empty;
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class Locations
{
    [XmlElement("Location", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public List<Location> Location { get; set; } = new();
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class Location
{
    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string WindowEndDate { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string WindowStartDate { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string TargetLocationIdentifier { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public ReferenceDataType? TargetLocationIdentifierType { get; set; }
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class ReferenceDataType
{
    [XmlAttribute("RefDataSetName")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement("Code", Namespace = "http://types.defra.gov.uk/ahw/common/referencedatasets")]
    public string Code { get; set; } = string.Empty;
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class SpeciesCodesAndAnimals
{
    [XmlElement("SpeciesCodeAndAnimals", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public List<SpeciesCodeAndAnimals> SpeciesCodeAndAnimalsList { get; set; } = new();
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class SpeciesCodeAndAnimals
{
    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public ReferenceDataType? AnimalSpecies { get; set; }

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public AnimalIdentifiers? AnimalIdentifiers { get; set; }
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class AnimalIdentifiers
{
    [XmlElement("AnimalIdentifier", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public List<AnimalIdentifier> AnimalIdentifier { get; set; } = new();
}

[XmlType(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
public class AnimalIdentifier
{
    [XmlElement("AnimalIdentifier", Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public string AnimalIdentifierValue { get; set; } = string.Empty;

    [XmlElement(Namespace = "http://services.defra.gov.uk/ahw/livestockmovements")]
    public ReferenceDataType? AnimalIdentifierType { get; set; }
}
