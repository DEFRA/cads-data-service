using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Requests;

public class GetGbCattleRegistrationsRequest : GetReportRequest
{
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("month")]
    public int Month { get; set; }
}