using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobStatus;

public class GetReportJobStatusQueryHandler(IMiReportJobRepository reportJobRepository)
    : IQueryHandler<GetReportJobStatusQuery, ReportJobStatusDto>
{
    public async Task<ReportJobStatusDto> Handle(GetReportJobStatusQuery request, CancellationToken cancellationToken)
    {
        var reportJob = await reportJobRepository.GetByReportJobIdAsync(request.ReportJobId, cancellationToken)
            ?? throw new NotFoundException(nameof(MiReportJob), request.ReportJobId);

        return new ReportJobStatusDto
        {
            JobId = reportJob.ReportJobId,
            Status = reportJob.Status,
            IsReady = reportJob.Status == ReportJobStatus.Complete,
            Error = reportJob.Error
        };
    }
}