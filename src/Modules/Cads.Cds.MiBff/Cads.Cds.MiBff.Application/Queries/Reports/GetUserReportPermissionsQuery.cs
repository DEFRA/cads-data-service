using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetUserReportPermissionsQuery : IQuery<UserReportPermissionsDto>
{
    public required string ExternalSubject { get; set; }
    public required string ReportKey { get; set; }
}