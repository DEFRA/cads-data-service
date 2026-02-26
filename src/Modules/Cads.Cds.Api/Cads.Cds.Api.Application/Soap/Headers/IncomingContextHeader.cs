using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Headers;

/// <summary>
/// Model for reading incoming DEFRA context headers
/// </summary>
[XmlRoot("context", Namespace = "http://types.defra.gov.uk/context")]
public class IncomingContextHeader
{
    [XmlElement("applicationContext")]
    public ApplicationContext? ApplicationContext { get; set; }

    [XmlElement("requestContext")]
    public RequestContext? RequestContext { get; set; }
}

public class ApplicationContext
{
    [XmlAttribute("applicationId")]
    public string ApplicationId { get; set; } = string.Empty;

    [XmlElement("keyValuePair")]
    public List<KeyValuePair> KeyValuePairs { get; set; } = new();
}

public class KeyValuePair
{
    [XmlAttribute("key")]
    public string Key { get; set; } = string.Empty;

    [XmlAttribute("value")]
    public string Value { get; set; } = string.Empty;
}

public class RequestContext
{
    [XmlAttribute("clientRequestId")]
    public string ClientRequestId { get; set; } = string.Empty;

    [XmlAttribute("clientTimestamp")]
    public string ClientTimestamp { get; set; } = string.Empty;
}