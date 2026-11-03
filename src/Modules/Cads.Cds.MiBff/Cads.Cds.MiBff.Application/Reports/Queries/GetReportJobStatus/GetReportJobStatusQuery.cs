using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobStatus;

public class GetReportJobStatusQuery : IQuery<ReportJobStatusDto>
{
    public required Guid ReportJobId { get; set; }
}

