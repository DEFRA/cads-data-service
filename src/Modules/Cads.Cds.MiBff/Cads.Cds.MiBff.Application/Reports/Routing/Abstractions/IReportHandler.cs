using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Reports.Requests;

namespace Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;

/// <summary>
/// This is the interface that the report handlers implement.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IReportHandler<in TRequest, TQuery, TResult>
    where TRequest : GetReportRequest
    where TQuery : IQuery<TResult>
{
    TQuery BuildQuery(TRequest request);
}

/// <summary>
/// This is the interface that the registry stores.
/// </summary>
public interface IReportHandler
{
    object BuildUntypedQuery(GetReportRequest request);
}