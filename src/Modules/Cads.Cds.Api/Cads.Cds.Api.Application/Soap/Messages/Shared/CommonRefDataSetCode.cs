using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.Shared;

public class CommonRefDataSetCode
{
    [XmlAttribute("RefDataSetName")]
    public string RefDataSetName { get; set; } = string.Empty;

    [XmlElement("Code", Namespace = Namespaces.CommonReferenceDataSetTypes)]
    public string Code { get; set; } = string.Empty;
}