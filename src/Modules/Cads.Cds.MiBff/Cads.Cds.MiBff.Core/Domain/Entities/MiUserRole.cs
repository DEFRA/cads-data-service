namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiUserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTimeOffset GrantedAt { get; set; }

    public MiUser User { get; set; } = null!;
    public MiRole Role { get; set; } = null!;
}