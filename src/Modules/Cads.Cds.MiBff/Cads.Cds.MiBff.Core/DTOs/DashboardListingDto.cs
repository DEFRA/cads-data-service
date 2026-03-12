namespace Cads.Cds.MiBff.Core.DTOs;

public class DashboardListingDto
{
    public string ReportId { get; set; }
    public string ReportKey { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}