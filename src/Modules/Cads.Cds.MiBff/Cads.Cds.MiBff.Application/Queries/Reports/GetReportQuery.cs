using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetReportQuery<T> : GetReportQueryBase, IQuery<T>
{
    public GetReportQuery(string reportKey)
    {
        ReportKey = reportKey;
    }
}