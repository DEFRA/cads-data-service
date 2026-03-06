using System.Xml.Serialization;

namespace Cads.Cds.Api.Application.Soap.Messages.CattleStatus;

/// <summary>
/// Response model for GetCattleStatus SOAP operation
/// </summary>
[XmlRoot("GetCattleStatusResponse", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/holding")]
public class GetCattleStatusResponse
{
    [XmlElement("HoldingId", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/holding")]
    public string HoldingId { get; set; } = string.Empty;

    [XmlElement("CattleStatusCSV", Namespace = "http://services.defra.gov.uk/ahw/tracing/cattle/holding")]
    public string CattleStatusCSV { get; set; } = string.Empty;
}