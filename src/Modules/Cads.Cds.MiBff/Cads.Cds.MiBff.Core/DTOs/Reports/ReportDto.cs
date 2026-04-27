using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class ReportDto
{
    [JsonPropertyName("reportId")]
    public required Guid ReportId { get; set; }

    [JsonPropertyName("reportKey")]
    public required string ReportKey { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}