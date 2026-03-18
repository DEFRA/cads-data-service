using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts.Adapters;
using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts;

public class GetCohortsByAnimalIdQueryHandler(CohortsQueryAdapter adapter)
    : JsonResponseDataQueryHandler<GetCohortsByAnimalIdQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetCohortsByAnimalIdQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}