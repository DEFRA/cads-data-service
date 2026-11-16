using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Reports.Mappings;

public static class ReportDtoMapper
{
    public static ReportDto Map(MiEffectiveReportPermission permission) => new()
    {
        ReportId = permission.ReportId,
        ReportKey = permission.ReportKey,
        Title = permission.Title,
        Description = permission.Description ?? string.Empty,
        IsActive = permission.IsActive
    };
}