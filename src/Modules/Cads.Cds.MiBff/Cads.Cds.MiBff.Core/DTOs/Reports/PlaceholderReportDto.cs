using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class PlaceholderReportDto
{
    [JsonPropertyName("reportKey")]
    public required string ReportKey { get; set; }
}
