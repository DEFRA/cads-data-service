using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Application.Reports.Requests;

public class GetGbCattleRegistrationsRequest : GetReportRequest
{
    public const string ExpectedKey = "gb_cattle_registrations";

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("month")]
    public int Month { get; set; }
}