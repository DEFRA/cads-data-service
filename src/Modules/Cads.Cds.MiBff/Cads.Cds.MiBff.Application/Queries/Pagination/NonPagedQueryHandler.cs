using MediatR;

namespace Cads.Cds.MiBff.Application.Queries.Pagination;

public abstract class NonPagedQueryHandler<TQuery, TItem>
    : IRequestHandler<TQuery, NonPaginatedResult<TItem>>
    where TQuery : INonPagedQuery<TItem>
{
    protected abstract Task<(List<TItem> Items, int TotalCount)> FetchAsync(TQuery query, CancellationToken cancellationToken);

    public async Task<NonPaginatedResult<TItem>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await FetchAsync(query, cancellationToken);

        return new NonPaginatedResult<TItem>
        {
            Count = totalCount,
            Values = items,
        };
    }
}