using System.Xml.Serialization;
using Cads.Cds.Api.Application.Soap.Messages.Shared;
using CoreWCF;

namespace Cads.Cds.Api.Application.Soap.Messages.CattleStatus;

[XmlRoot("GetCattleStatusRequest", Namespace = Namespaces.CattleHolding)]
[MessageContract(WrapperName = "GetCattleStatusRequest", WrapperNamespace = Namespaces.CattleHolding, IsWrapped = true)]
public class GetCattleStatusRequest
{
    [XmlElement("HoldingId", Namespace = Namespaces.CattleHolding)]
    [MessageBodyMember(Namespace = Namespaces.CattleHolding, Order = 0)]
    public string HoldingId { get; set; } = string.Empty;
}