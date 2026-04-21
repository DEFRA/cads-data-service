using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Ukv.Movements.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Movements;

public class GetMovementsQueryHandler(MovementsQueryAdapter adapter)
     : PagedQueryHandler<GetMovementsQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetMovementsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}