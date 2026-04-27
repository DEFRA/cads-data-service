namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class UserReportPermissionsDto
{
    public required string ReportKey { get; set; }
    public List<string> Permissions { get; set; } = [];
}