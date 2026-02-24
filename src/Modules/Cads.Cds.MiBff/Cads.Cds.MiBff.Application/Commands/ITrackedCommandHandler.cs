using Cads.Cds.MiBff.Core.Domain.BuildingBlocks.Aggregates;
using MediatR;

namespace Cads.Cds.MiBff.Application.Commands;

public interface ITrackedCommandHandler<in TCommand, TResult>
    : IRequestHandler<TCommand, TrackedResult<TResult>> where TCommand
    : ICommand<TrackedResult<TResult>>
{
}