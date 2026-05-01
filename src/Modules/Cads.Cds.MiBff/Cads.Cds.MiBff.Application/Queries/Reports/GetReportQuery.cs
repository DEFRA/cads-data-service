using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetReportQuery<T>(string reportKey) : IQuery<T>
{
    public string ReportKey { get; init; } = reportKey;
}
