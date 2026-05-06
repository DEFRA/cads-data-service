using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;

namespace Cads.Cds.MiBff.Application.Reports.Routing;

public abstract class ReportHandlerBase<TRequest, TQuery, TResult> : IReportHandler
    where TRequest : GetReportRequest
    where TQuery : IQuery<TResult>
{
    public abstract TQuery BuildQuery(TRequest request);

    public object BuildUntypedQuery(GetReportRequest request)
        => BuildQuery((TRequest)request);
}
