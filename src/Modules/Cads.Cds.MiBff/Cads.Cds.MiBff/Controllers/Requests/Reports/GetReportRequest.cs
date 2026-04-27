using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Controllers.Requests.Reports;

public class GetReportRequest
{
    [JsonPropertyName("reportKey")]
    public string ReportKey { get; set; } = string.Empty;
}