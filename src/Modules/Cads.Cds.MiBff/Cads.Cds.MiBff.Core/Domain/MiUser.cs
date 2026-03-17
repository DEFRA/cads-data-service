namespace Cads.Cds.MiBff.Core.Domain;

public class MiUser
{
    public Guid UserId { get; set; }

    public ICollection<MiUserRole> UserRoles { get; set; } = new List<MiUserRole>();
}