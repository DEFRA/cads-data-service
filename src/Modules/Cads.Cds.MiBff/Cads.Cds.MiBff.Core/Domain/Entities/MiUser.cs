namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiUser
{
    public Guid UserId { get; set; }
    public string ExternalSubject { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<MiUserRole> UserRoles { get; set; } = new List<MiUserRole>();
    public ICollection<MiUserReportPermission> UserReportPermissions { get; set; } = new List<MiUserReportPermission>();
}