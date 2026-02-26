using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace Cads.Cds.Api.Application.Soap.Headers;

/// <summary>
/// Custom SOAP context header for DEFRA context information
/// </summary>
[DataContract(Name = "context", Namespace = "http://types.defra.gov.uk/context")]
public class SoapContextHeader : MessageHeader
{
    private readonly string _applicationId;
    private readonly string _environment;
    private readonly string _clientRequestId;
    private readonly string _clientTimestamp;

    public SoapContextHeader(
        string applicationId,
        string environment,
        string clientRequestId,
        string clientTimestamp)
    {
        _applicationId = applicationId;
        _environment = environment;
        _clientRequestId = clientRequestId;
        _clientTimestamp = clientTimestamp;
    }

    public override string Name => "context";

    public override string Namespace => "http://types.defra.gov.uk/context";

    protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
    {
        // Write applicationContext
        writer.WriteStartElement("applicationContext", Namespace);
        writer.WriteAttributeString("applicationId", _applicationId);

        writer.WriteStartElement("keyValuePair", Namespace);
        writer.WriteAttributeString("key", "ENVIRONMENT");
        writer.WriteAttributeString("value", _environment);
        writer.WriteEndElement();

        writer.WriteStartElement("keyValuePair", Namespace);
        writer.WriteAttributeString("key", "CORPORATEUSERID");
        writer.WriteAttributeString("value", "");
        writer.WriteEndElement();

        writer.WriteStartElement("keyValuePair", Namespace);
        writer.WriteAttributeString("key", "CORRELATIONID");
        writer.WriteAttributeString("value", "");
        writer.WriteEndElement();

        writer.WriteEndElement(); // applicationContext

        // Write requestContext
        writer.WriteStartElement("requestContext", Namespace);
        writer.WriteAttributeString("clientRequestId", _clientRequestId);
        writer.WriteAttributeString("clientTimestamp", _clientTimestamp);
        writer.WriteEndElement();
    }
}