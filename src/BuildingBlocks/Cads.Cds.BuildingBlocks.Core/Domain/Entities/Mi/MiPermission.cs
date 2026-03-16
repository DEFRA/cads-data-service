namespace Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

public class MiPermission
{
    public Guid PermissionId { get; set; }
    public string PermissionKey { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<MiRoleReportPermission> RoleReportPermissions { get; set; } = new List<MiRoleReportPermission>();
    public ICollection<MiUserReportPermission> UserReportPermissions { get; set; } = new List<MiUserReportPermission>();

    // reference to effective permissions, not mapped to database, used for permission evaluation results
    public ICollection<MiEffectiveReportPermission> EffectiveReportPermissions { get; set; } = new List<MiEffectiveReportPermission>();
}