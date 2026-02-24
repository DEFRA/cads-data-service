using MediatR;

namespace Cads.Cds.MiBff.Application.Commands;

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}