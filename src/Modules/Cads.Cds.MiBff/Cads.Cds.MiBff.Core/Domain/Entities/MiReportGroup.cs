namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiReportGroup
{
    public Guid GroupId { get; set; }
    public string GroupKey { get; set; } = null!;
    public string Title { get; set; } = null!;

    public ICollection<MiReportGroupMap> ReportGroupMaps { get; set; } = new List<MiReportGroupMap>();
}