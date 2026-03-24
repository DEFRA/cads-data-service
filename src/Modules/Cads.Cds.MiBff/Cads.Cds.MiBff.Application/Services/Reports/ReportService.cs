using AutoMapper;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class ReportService(
    IMiEffectiveReportPermissionRepository effectiveReportPermissionRepository,
    IMapper mapper) : IReportService
{
    [ExcludeFromCodeCoverage]
    public async Task<IEnumerable<ReportDto>> GetUserReportsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<ReportDto>>(s_reports).Result;
    }

    public async Task<IEnumerable<ReportDto>> GetUserReportsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var permissions = await effectiveReportPermissionRepository.GetByUserEmailAsync(email, cancellationToken);

        return mapper.Map<IEnumerable<ReportDto>>(permissions);
    }

    private static readonly IReadOnlyList<ReportDto> s_reports =
    [
        ReportItem(Guid.NewGuid(), "holding_summary", "Holding summary", "Summary of a single holding, including on/off movements, species and mapped holdings."),
        ReportItem(Guid.NewGuid(), "animal_summary", "Animal summary", "Movement history and mapped journey for an individual animal, including last known holding"),
        ReportItem(Guid.NewGuid(), "movements_all_holdings", "Movements (all holdings)", "High-level movement metrics including within-country and cross-border flows and detailed movement table."),
        ReportItem(Guid.NewGuid(), "movement_summary_holding", "Movement summary (holding)", "On/off movement summaries for a holding, including direction and species charts and tables."),
        ReportItem(Guid.NewGuid(), "journey_by_haulier", "Journey by haulier", "Journey-level view by vehicle and haulier, including load/unload summary and mapped holdings."),
        ReportItem(Guid.NewGuid(), "zonal_movements_summary", "Zonal movements summary", "Into-zone, out-of-zone, within-zone, import and export summaries with mapped holdings in and around the zone."),
        ReportItem(Guid.NewGuid(), "cohort_tracing", "Cohort tracing", "Cohort holdings, status summary and detailed animal/last-known holding data for a selected cohort animal."),
        ReportItem(Guid.NewGuid(), "sheep_and_goat_inspections", "Sheep and goat inspections", "Inspection-oriented view of holding movement history and animals in movement around inspection dates."),
        ReportItem(Guid.NewGuid(), "unregistered_herds_and_flocks", "Unregistered herds and flocks", "Data quality view of unregistered and archived herds/flocks, with related holding metrics."),
        ReportItem(Guid.NewGuid(), "scrapie_flock_scheme_audit", "Scrapie flock scheme audit", "Compulsory Scrapie Flock Scheme audit, including holding details, movement history and off-move summary.")
    ];

    [ExcludeFromCodeCoverage]
    private static ReportDto ReportItem(Guid reportId, string reportKey, string title, string description)
    {
        return new ReportDto
        {
            ReportId = reportId,
            ReportKey = reportKey,
            Title = title,
            Description = description,
            IsActive = true
        };
    }
}