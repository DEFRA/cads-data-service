using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.Shared;

public class TraceIdentifier
{
    [XmlElement("TraceSpecificationIdentifier", Namespace = "")]
    public string TraceSpecificationIdentifier { get; set; } = string.Empty;

    [XmlElement("TraceIdentifier", Namespace = "")]
    public string TraceIdentifierValue { get; set; } = string.Empty;
}