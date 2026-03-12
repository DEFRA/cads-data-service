using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;
using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.CattleStatus;

/// <summary>
/// Response model for GetCattleStatus SOAP operation
/// </summary>
[XmlRoot("GetCattleStatusResponse", Namespace = Namespaces.CattleHolding)]
[MessageContract(WrapperName = "GetCattleStatusResponse", WrapperNamespace = Namespaces.CattleHolding, IsWrapped = true)]
public class GetCattleStatusResponse
{
    [XmlElement("HoldingId", Namespace = Namespaces.CattleHolding)]
    [MessageBodyMember(Namespace = "", Order = 0)]
    public string HoldingId { get; set; } = string.Empty;

    [XmlElement("CattleStatusCSV", Namespace = Namespaces.CattleHolding)]
    [MessageBodyMember(Namespace = "", Order = 1)]
    public string CattleStatusCSV { get; set; } = string.Empty;
}