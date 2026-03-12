using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Services;

public class ReportService : IReportService
{
    public Task<IEnumerable<ReportListingDto>> GetUserReportListAsync(string? queryUserId, CancellationToken cancellationToken)
    {
        // TODO get from DB, filter to reports that user has permission for //NOSONAR
        return Task.FromResult(
            (IEnumerable<ReportListingDto>)new List<ReportListingDto>()
            {
                ReportItem("Holding", "dashboard_1", "Holding summary", "Summary of a single holding, including on/off movements, species and mapped holdings."),
                ReportItem("Animal", "dashboard_1", "Animal summary", "Movement history and mapped journey for an individual animal, including last known holding"),
                ReportItem("Movements", "dashboard_3", "Movements (all holdings)", "High-level movement metrics including within-country and cross-border flows and detailed movement table."),
                ReportItem("Movement", "dashboard_4", "Movement summary (holding)", "On/off movement summaries for a holding, including direction and species charts and tables."),
                ReportItem("Journey", "dashboard_5", "Journey by haulier", "Journey-level view by vehicle and haulier, including load/unload summary and mapped holdings."),
                ReportItem("Zonal", "dashboard_6", "Zonal movements summary", "Into-zone, out-of-zone, within-zone, import and export summaries with mapped holdings in and around the zone."),
                ReportItem("Cohort", "dashboard_7", "Cohort tracing", "Cohort holdings, status summary and detailed animal/last-known holding data for a selected cohort animal."),
                ReportItem("Sheep", "dashboard_8", "Sheep and goat inspections", "Inspection-oriented view of holding movement history and animals in movement around inspection dates."),
                ReportItem("Unregistered", "dashboard_9", "Unregistered herds and flocks", "Data quality view of unregistered and archived herds/flocks, with related holding metrics."),
                ReportItem("Scrapie", "dashboard_10", "Scrapie flock scheme audit", "Compulsory Scrapie Flock Scheme audit, including holding details, movement history and off-move summary.")
            });
    }

    private static ReportListingDto ReportItem(string id, string key, string title, string description)
    {
        return new ReportListingDto()
        {
            ReportId = id,
            ReportKey = key,
            Title = title,
            Description = description,
            IsActive = true
        };
    }
}