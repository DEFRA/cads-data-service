namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiEffectiveReportPermission
{
    public Guid ReportId { get; set; }

    public required string ReportKey { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string? DisplayName { get; set; }

    public required string ExternalSubject { get; set; }

    public bool Granted { get; set; }
}