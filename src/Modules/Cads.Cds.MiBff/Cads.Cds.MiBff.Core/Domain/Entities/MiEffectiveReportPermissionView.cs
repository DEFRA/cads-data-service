namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiEffectiveReportPermissionView
{
    public required string Email { get; set; }

    public required string ReportKey { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public bool Granted { get; set; }
}