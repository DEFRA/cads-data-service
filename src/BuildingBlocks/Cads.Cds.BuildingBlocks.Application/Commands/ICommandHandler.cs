using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Commands;

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}