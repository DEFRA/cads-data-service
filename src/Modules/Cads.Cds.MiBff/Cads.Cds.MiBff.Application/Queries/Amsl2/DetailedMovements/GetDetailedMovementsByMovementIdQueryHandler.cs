using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements;

public class GetDetailedMovementsByMovementIdQueryHandler(DetailedMovementsQueryAdapter adapter)
    : DefaultQueryHandler<GetDetailedMovementsByMovementIdQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetDetailedMovementsByMovementIdQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}