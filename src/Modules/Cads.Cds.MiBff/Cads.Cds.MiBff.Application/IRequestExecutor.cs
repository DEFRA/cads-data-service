using Cads.Cds.MiBff.Application.Commands;
using Cads.Cds.MiBff.Application.Queries;
using Cads.Cds.MiBff.Core.Domain.BuildingBlocks.Aggregates;

namespace Cads.Cds.MiBff.Application;

public interface IRequestExecutor
{
    Task<TResponse> ExecuteCommand<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    Task<TResponse> ExecuteCommand<TResponse>(ICommand<TrackedResult<TResponse>> command, CancellationToken cancellationToken = default);
    Task<TrackedResult<TResponse>> ExecuteTrackedCommand<TResponse>(ICommand<TrackedResult<TResponse>> command, CancellationToken cancellationToken = default);
    Task<TResponse> ExecuteQuery<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}
