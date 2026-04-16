using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetHoldingSummaryReportQuery : IQuery<HoldingSummaryReportDto>
{
    public required string ReportKey { get; set; }
}
