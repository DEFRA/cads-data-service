using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Queries;

public abstract class QueryHandler<TQuery, TDocument> : IRequestHandler<TQuery, IEnumerable<TDocument>>
    where TQuery : IRequest<IEnumerable<TDocument>>
{
    protected abstract Task<IEnumerable<TDocument>> FetchAsync(TQuery request, CancellationToken cancellationToken);

    public async Task<IEnumerable<TDocument>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        return await FetchAsync(request, cancellationToken);
    }
}