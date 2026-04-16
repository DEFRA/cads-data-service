using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class HoldingSummaryReportDto
{
    [JsonPropertyName("reportId")]
    public Guid ReportId { get; set; }
}
