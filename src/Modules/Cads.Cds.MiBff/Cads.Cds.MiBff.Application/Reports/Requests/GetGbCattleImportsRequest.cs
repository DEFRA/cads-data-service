using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Requests;

public class GetGbCattleImportsRequest : GetReportRequest
{
    [JsonPropertyName("year")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Year { get; set; }
}