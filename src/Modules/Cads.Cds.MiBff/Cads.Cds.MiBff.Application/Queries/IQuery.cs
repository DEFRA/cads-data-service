using MediatR;

namespace Cads.Cds.MiBff.Application.Queries;

public interface IQuery<TResponse> : IRequest<TResponse> { }