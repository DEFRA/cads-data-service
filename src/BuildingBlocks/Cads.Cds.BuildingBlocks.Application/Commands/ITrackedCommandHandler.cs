using Cads.Cds.BuildingBlocks.Core.Domain.Aggregates;
using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Commands;

public interface ITrackedCommandHandler<in TCommand, TResult>
    : IRequestHandler<TCommand, TrackedResult<TResult>> where TCommand
    : ICommand<TrackedResult<TResult>>
{
}