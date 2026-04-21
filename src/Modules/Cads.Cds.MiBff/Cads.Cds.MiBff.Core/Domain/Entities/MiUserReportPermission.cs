namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiUserReportPermission
{
    public Guid UserId { get; set; }
    public Guid ReportId { get; set; }
    public Guid PermissionId { get; set; }
    public bool Granted { get; set; }
    public string? Reason { get; set; }
    public DateTimeOffset GrantedAt { get; set; }

    public MiUser User { get; set; } = null!;
    public MiReport Report { get; set; } = null!;
    public MiPermission Permission { get; set; } = null!;
}