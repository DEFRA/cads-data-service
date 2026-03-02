using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages;

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
    public string DateOfBirth { get; set; } = string.Empty;

}