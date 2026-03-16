namespace Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

public class MiRoleReportPermission
{
    public Guid RoleId { get; set; }
    public Guid ReportId { get; set; }
    public Guid PermissionId { get; set; }
    public bool Granted { get; set; }

    public MiRole Role { get; set; } = null!;
    public MiReport Report { get; set; } = null!;
    public MiPermission Permission { get; set; } = null!;
}