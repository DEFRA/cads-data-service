using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements;

public class GetDetailedMovementsQueryHandler(DetailedMovementsQueryAdapter adapter)
    : PagedQueryHandler<GetDetailedMovementsQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetDetailedMovementsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}