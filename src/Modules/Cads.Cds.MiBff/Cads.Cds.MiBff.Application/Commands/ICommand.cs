using MediatR;

namespace Cads.Cds.MiBff.Application.Commands;

public interface ICommand<TResponse> : IRequest<TResponse> { }