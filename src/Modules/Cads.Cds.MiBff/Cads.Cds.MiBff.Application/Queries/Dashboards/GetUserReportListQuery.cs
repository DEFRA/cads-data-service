using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Dashboards;

public class GetUserReportListQuery : IQuery<IEnumerable<ReportListingDto>>
{
    public string? UserId { get; set; }
}