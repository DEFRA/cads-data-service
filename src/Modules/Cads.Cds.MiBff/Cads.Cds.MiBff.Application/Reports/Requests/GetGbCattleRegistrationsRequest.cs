using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Requests;

public class GetGbCattleRegistrationsRequest : GetReportRequest
{
    [JsonPropertyName("year")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Year { get; set; }

    [JsonPropertyName("month")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Month { get; set; }
}