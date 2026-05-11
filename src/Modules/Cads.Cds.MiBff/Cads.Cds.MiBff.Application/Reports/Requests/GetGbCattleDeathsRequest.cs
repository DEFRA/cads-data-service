using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Requests;

public class GetGbCattleDeathsRequest : GetReportRequest
{
    [JsonPropertyName("year")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Year { get; set; }
}