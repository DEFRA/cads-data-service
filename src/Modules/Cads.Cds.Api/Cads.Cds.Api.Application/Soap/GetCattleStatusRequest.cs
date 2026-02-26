using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap;

/// <summary>
/// Request model for GetCattleStatus SOAP operation
/// </summary>
[XmlType(Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/holding")]
public class GetCattleStatusRequest
{
    public string HoldingId { get; set; } = string.Empty;
}