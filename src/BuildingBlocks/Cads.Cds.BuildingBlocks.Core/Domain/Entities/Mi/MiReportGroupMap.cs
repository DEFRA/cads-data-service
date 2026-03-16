namespace Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

public class MiReportGroupMap
{
    public Guid GroupId { get; set; }
    public Guid ReportId { get; set; }

    public MiReportGroup Group { get; set; } = null!;
    public MiReport Report { get; set; } = null!;
}