using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

public abstract class PagedQueryHandler<TQuery, TDocument>
    : IRequestHandler<TQuery, PaginatedResult<TDocument>>
    where TQuery : IPagedQuery<TDocument>
{
    protected abstract Task<(IEnumerable<TDocument> Items, int TotalCount)> FetchAsync(TQuery request, CancellationToken cancellationToken);

    public async Task<PaginatedResult<TDocument>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await FetchAsync(request, cancellationToken);

        return new PaginatedResult<TDocument>
        {
            Results = items,
            Count = items.Count(),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}