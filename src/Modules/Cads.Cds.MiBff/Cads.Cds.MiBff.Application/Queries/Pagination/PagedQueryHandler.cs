using MediatR;

namespace Cads.Cds.MiBff.Application.Queries.Pagination;

public abstract class PagedQueryHandler<TQuery, TItem>
    : IRequestHandler<TQuery, PaginatedResult<TItem>>
    where TQuery : IPagedQuery<TItem>
{
    protected abstract Task<(List<TItem> Items, int TotalCount)> FetchAsync(TQuery query, CancellationToken cancellationToken);

    public async Task<PaginatedResult<TItem>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await FetchAsync(query, cancellationToken);

        return new PaginatedResult<TItem>
        {
            Count = totalCount,
            Values = items,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}