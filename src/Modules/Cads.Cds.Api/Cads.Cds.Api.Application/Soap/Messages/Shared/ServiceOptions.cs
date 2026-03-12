using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.Shared;

public class ServiceOptions
{
    [XmlElement("DestinationDataBaseName")]
    public string DestinationDataBaseName { get; set; } = string.Empty;
    [XmlElement("DestinationStoredProcedure")]
    public string DestinationStoredProcedure { get; set; } = string.Empty;
}