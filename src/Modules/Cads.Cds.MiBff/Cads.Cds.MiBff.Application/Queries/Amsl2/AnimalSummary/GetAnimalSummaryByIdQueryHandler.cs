using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary;

public class GetAnimalSummaryByAnimalIdQueryHandler(AnnualSummaryQueryAdapter adapter)
    : JsonResponseDataQueryHandler<GetAnimalSummaryByAnimalIdQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetAnimalSummaryByAnimalIdQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}