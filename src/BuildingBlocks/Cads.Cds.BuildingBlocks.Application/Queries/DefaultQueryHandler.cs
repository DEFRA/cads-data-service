using MediatR;

namespace Cads.Cds.BuildingBlocks.Application.Queries;

public abstract class DefaultQueryHandler<TQuery, TItem>
    : IRequestHandler<TQuery, DefaultResult<TItem>>
    where TQuery : IDefaultQuery<TItem>
{
    protected abstract Task<(IEnumerable<TItem> Items, int TotalCount)> FetchAsync(TQuery request, CancellationToken cancellationToken);

    public async Task<DefaultResult<TItem>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var (items, _) = await FetchAsync(request, cancellationToken);

        return new DefaultResult<TItem>
        {
            Results = items,
        };
    }
}