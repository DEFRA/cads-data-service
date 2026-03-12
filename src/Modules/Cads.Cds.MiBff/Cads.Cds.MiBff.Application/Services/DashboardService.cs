using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Services;

public class DashboardService : IDashboardService
{
    public Task<IEnumerable<DashboardListingDto>> GetAllForUserAsync(string? queryUserId, CancellationToken cancellationToken)
    {
        // TODO get from DB, filter to reports that user has permission for //NOSONAR
        return Task.FromResult(
            (IEnumerable<DashboardListingDto>)new List<DashboardListingDto>()
            {
                   new DashboardListingDto() {
                       ReportId = "Holding",
                       ReportKey = "dashboard_1",
                       Title = "Holding summary",
                       Description = "Summary of a single holding, including on/off movements, species and mapped holdings.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Animal",
                       ReportKey = "dashboard_2",
                       Title = "Animal summary",
                       Description = "Movement history and mapped journey for an individual animal, including last known holding.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Movements",
                       ReportKey = "dashboard_3",
                       Title = "Movements (all holdings)",
                       Description = "High-level movement metrics including within-country and cross-border flows and detailed movement table.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Movement",
                       ReportKey = "dashboard_4",
                       Title = "Movement summary (holding)",
                       Description = "On/off movement summaries for a holding, including direction and species charts and tables.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Journey",
                       ReportKey = "dashboard_5",
                       Title = "Journey by haulier",
                       Description = "Journey-level view by vehicle and haulier, including load/unload summary and mapped holdings.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Zonal",
                       ReportKey = "dashboard_6",
                       Title = "Zonal movements summary",
                       Description = "Into-zone, out-of-zone, within-zone, import and export summaries with mapped holdings in and around the zone.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Cohort",
                       ReportKey = "dashboard_7",
                       Title = "Cohort tracing",
                       Description = "Cohort holdings, status summary and detailed animal/last-known holding data for a selected cohort animal.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Sheep",
                       ReportKey = "dashboard_8",
                       Title = "Sheep and goat inspections",
                       Description = "Inspection-oriented view of holding movement history and animals in movement around inspection dates.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Unregistered",
                       ReportKey = "dashboard_9",
                       Title = "Unregistered herds and flocks",
                       Description = "Data quality view of unregistered and archived herds/flocks, with related holding metrics.",
                       IsActive = true
                   },
                   new DashboardListingDto() {
                       ReportId = "Scrapie",
                       ReportKey = "dashboard_10",
                       Title = "Scrapie flock scheme audit",
                       Description = "Compulsory Scrapie Flock Scheme audit, including holding details, movement history and off-move summary.",
                       IsActive = true
                   }
            });
    }
}