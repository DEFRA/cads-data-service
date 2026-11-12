using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobFile;

public class GetReportJobFileQuery : IQuery<IReportResult>
{
    public required string ReportKey { get; init; }
    public required Guid JobId { get; init; }
    public ReportFileType FileType { get; init; } = ReportFileType.Xlsx;
}

