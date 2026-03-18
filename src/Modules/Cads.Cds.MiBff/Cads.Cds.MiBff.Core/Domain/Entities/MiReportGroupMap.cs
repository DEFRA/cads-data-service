namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiReportGroupMap
{
    public Guid GroupId { get; set; }
    public Guid ReportId { get; set; }

    public MiReportGroup Group { get; set; } = null!;
    public MiReport Report { get; set; } = null!;
}