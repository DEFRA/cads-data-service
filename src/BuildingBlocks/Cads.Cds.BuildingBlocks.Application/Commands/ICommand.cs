using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse> { }