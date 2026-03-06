using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.Shared;

public class ServiceOptions
{
    [XmlElement(Namespace = "")]
    public string DestinationDataBaseName { get; set; } = string.Empty;

    [XmlElement(Namespace = "")]
    public string DestinationStoredProcedure { get; set; } = string.Empty;
}