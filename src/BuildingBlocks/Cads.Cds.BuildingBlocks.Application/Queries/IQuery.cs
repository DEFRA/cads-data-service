using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> { }