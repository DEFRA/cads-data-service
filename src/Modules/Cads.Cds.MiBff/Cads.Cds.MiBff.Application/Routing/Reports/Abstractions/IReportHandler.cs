using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.MiBff.Application.Routing.Reports.Abstractions;

public interface IReportHandler<in TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
