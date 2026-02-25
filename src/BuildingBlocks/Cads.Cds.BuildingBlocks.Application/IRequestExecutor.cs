using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;

namespace Cads.Cds.BuildingBlocks.Application;

public interface IRequestExecutor
{
    Task<TResponse> ExecuteCommand<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    Task<TResponse> ExecuteCommand<TResponse>(ICommand<TrackedResult<TResponse>> command, CancellationToken cancellationToken = default);
    Task<TrackedResult<TResponse>> ExecuteTrackedCommand<TResponse>(ICommand<TrackedResult<TResponse>> command, CancellationToken cancellationToken = default);
    Task<TResponse> ExecuteQuery<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}