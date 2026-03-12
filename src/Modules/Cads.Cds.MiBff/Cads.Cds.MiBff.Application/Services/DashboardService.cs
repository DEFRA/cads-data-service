using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Services;

public class DashboardService : IDashboardService
{
    public Task<IEnumerable<DashboardListingDto>> GetAllForUserAsync(string? queryUserId, CancellationToken cancellationToken)
    {
        // TODO get from DB, filter to reports that user has permission for //NOSONAR
        return Task.FromResult( //NOSONAR
            (IEnumerable<DashboardListingDto>)new List<DashboardListingDto>() //NOSONAR
            { //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Holding", //NOSONAR
                       ReportKey = "dashboard_1", //NOSONAR
                       Title = "Holding summary", //NOSONAR
                       Description = "Summary of a single holding, including on/off movements, species and mapped holdings.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Animal", //NOSONAR
                       ReportKey = "dashboard_2", //NOSONAR
                       Title = "Animal summary", //NOSONAR
                       Description = "Movement history and mapped journey for an individual animal, including last known holding.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Movements", //NOSONAR
                       ReportKey = "dashboard_3", //NOSONAR
                       Title = "Movements (all holdings)", //NOSONAR
                       Description = "High-level movement metrics including within-country and cross-border flows and detailed movement table.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Movement", //NOSONAR
                       ReportKey = "dashboard_4", //NOSONAR
                       Title = "Movement summary (holding)", //NOSONAR
                       Description = "On/off movement summaries for a holding, including direction and species charts and tables.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Journey", //NOSONAR
                       ReportKey = "dashboard_5", //NOSONAR
                       Title = "Journey by haulier", //NOSONAR
                       Description = "Journey-level view by vehicle and haulier, including load/unload summary and mapped holdings.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Zonal", //NOSONAR
                       ReportKey = "dashboard_6", //NOSONAR
                       Title = "Zonal movements summary", //NOSONAR
                       Description = "Into-zone, out-of-zone, within-zone, import and export summaries with mapped holdings in and around the zone.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Cohort", //NOSONAR
                       ReportKey = "dashboard_7", //NOSONAR
                       Title = "Cohort tracing", //NOSONAR
                       Description = "Cohort holdings, status summary and detailed animal/last-known holding data for a selected cohort animal.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Sheep", //NOSONAR
                       ReportKey = "dashboard_8", //NOSONAR
                       Title = "Sheep and goat inspections", //NOSONAR
                       Description = "Inspection-oriented view of holding movement history and animals in movement around inspection dates.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Unregistered", //NOSONAR
                       ReportKey = "dashboard_9", //NOSONAR
                       Title = "Unregistered herds and flocks", //NOSONAR
                       Description = "Data quality view of unregistered and archived herds/flocks, with related holding metrics.", //NOSONAR
                       IsActive = true //NOSONAR
                   }, //NOSONAR
                   new DashboardListingDto() { //NOSONAR
                       ReportId = "Scrapie", //NOSONAR
                       ReportKey = "dashboard_10", //NOSONAR
                       Title = "Scrapie flock scheme audit", //NOSONAR
                       Description = "Compulsory Scrapie Flock Scheme audit, including holding details, movement history and off-move summary.", //NOSONAR
                       IsActive = true //NOSONAR
                   } //NOSONAR
            });
    }
}