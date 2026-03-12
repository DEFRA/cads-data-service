using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Core.DTOs;

public class DashboardListingDto
{
    [JsonRequired]
    public required string ReportId { get; set; }
    [JsonRequired]
    public required string ReportKey { get; set; }
    [JsonRequired]
    public required string Title { get; set; }
    [JsonRequired]
    public required string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}