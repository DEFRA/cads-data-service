namespace Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

public class MiReport
{
    public Guid ReportId { get; set; }
    public string ReportKey { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<MiReportGroupMap> ReportGroupMaps { get; set; } = new List<MiReportGroupMap>();
    public ICollection<MiRoleReportPermission> RoleReportPermissions { get; set; } = new List<MiRoleReportPermission>();
    public ICollection<MiUserReportPermission> UserReportPermissions { get; set; } = new List<MiUserReportPermission>();

    // reference to effective permissions, not mapped to database, used for permission evaluation results
    public ICollection<MiEffectiveReportPermission> EffectiveReportPermissions { get; set; } = new List<MiEffectiveReportPermission>();

}