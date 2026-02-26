using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace Cads.Cds.Api.Application.Soap.Headers;

/// <summary>
/// Custom SOAP Security header to handle WS-Security UsernameToken
/// </summary>
[DataContract(Name = "Security", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
public class SoapSecurityHeader : MessageHeader
{
    private readonly string _username;
    private readonly string _role;

    public SoapSecurityHeader(string username, string role = "user")
    {
        _username = username;
        _role = role;
    }

    public override string Name => "Security";

    public override string Namespace => "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

    protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
        writer.WriteStartElement("UsernameToken", Namespace);
        writer.WriteElementString("Username", Namespace, _username);
        writer.WriteEndElement();
    }

    protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
        writer.WriteStartElement(Name, Namespace);
        writer.WriteAttributeString("role", "http://www.w3.org/2003/05/soap-envelope", _role);
    }

    [DataMember]
    public string Username => _username;
}