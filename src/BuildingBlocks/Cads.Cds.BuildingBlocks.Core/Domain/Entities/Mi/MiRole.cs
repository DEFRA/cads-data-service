namespace Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

public class MiRole
{
    public Guid RoleId { get; set; }
    public string RoleKey { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<MiUserRole> UserRoles { get; set; } = new List<MiUserRole>();
    public ICollection<MiRoleReportPermission> RoleReportPermissions { get; set; } = new List<MiRoleReportPermission>();
}