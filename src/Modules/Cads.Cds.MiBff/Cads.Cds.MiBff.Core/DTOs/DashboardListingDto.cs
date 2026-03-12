using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs;

public class DashboardListingDto
{
    [JsonPropertyName("reportId")]
    public required string ReportId { get; set; }

    [JsonPropertyName("reportKey")]
    public required string ReportKey { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}