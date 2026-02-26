using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Headers;

/// <summary>
/// Model for reading incoming WS-Security headers
/// </summary>
[XmlRoot("Security", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
public class IncomingSecurityHeader
{
    [XmlElement("UsernameToken")]
    public UsernameToken? UsernameToken { get; set; }

    [XmlAttribute("role", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public string Role { get; set; } = string.Empty;
}

public class UsernameToken
{
    [XmlElement("Username")]
    public string Username { get; set; } = string.Empty;
}