namespace Cads.Cds.MiBff.Core.DTOs;

public class DashboardListingDto
{
    public required string ReportId { get; set; }
    public required string ReportKey { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}