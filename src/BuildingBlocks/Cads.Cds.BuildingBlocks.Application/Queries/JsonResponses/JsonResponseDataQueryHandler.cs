using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;

public abstract class JsonResponseDataQueryHandler<TQuery, TItem>
    : IRequestHandler<TQuery, JsonResponseDataResult<TItem>>
    where TQuery : IJsonResponseDataQuery<TItem>
{
    protected abstract Task<(IEnumerable<TItem> Items, int TotalCount)> FetchAsync(TQuery request, CancellationToken cancellationToken);

    public async Task<JsonResponseDataResult<TItem>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var (items, _) = await FetchAsync(request, cancellationToken);

        return new JsonResponseDataResult<TItem>
        {
            Results = items,
        };
    }
}