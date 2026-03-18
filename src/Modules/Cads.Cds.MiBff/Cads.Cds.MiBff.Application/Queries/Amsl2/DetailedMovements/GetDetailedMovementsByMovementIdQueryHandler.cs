using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements.Adapters;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements;

public class GetDetailedMovementsByMovementIdQueryHandler(DetailedMovementsQueryAdapter adapter)
    : JsonResponseDataQueryHandler<GetDetailedMovementsByMovementIdQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetDetailedMovementsByMovementIdQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}