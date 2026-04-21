using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetUserReportsQuery : IQuery<IEnumerable<ReportDto>>
{
    public required string Identifier { get; set; }
}