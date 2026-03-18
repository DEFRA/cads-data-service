namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiEffectiveReportPermission
{
    public Guid UserId { get; set; }
    public Guid ReportId { get; set; }
    public Guid PermissionId { get; set; }
    public bool Granted { get; set; }

    // Navigation props for convenience
    public MiUser? User { get; set; }
    public MiReport? Report { get; set; }
    public MiPermission? Permission { get; set; }
}