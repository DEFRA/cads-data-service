using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.Shared;

public class RefDataSetCode
{
    [XmlAttribute("RefDataSetName")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement("Code", Namespace = Namespaces.ReferenceDataSetTypes)]
    public string Code { get; set; } = string.Empty;
}