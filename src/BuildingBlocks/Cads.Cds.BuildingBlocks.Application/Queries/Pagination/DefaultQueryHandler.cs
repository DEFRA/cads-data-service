using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

public abstract class DefaultQueryHandler<TQuery, TItem>
    : IRequestHandler<TQuery, DefaultResult<TItem>>
    where TQuery : IDefaultQuery<TItem>
{
    protected abstract Task<(IEnumerable<TItem> Items, int TotalCount)> FetchAsync(TQuery query, CancellationToken cancellationToken);

    public async Task<DefaultResult<TItem>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var (items, _) = await FetchAsync(query, cancellationToken);

        return new DefaultResult<TItem>
        {
            Results = items,
        };
    }
}