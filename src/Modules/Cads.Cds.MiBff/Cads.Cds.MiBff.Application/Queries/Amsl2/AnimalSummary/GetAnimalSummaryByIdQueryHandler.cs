using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary;

public class GetAnimalSummaryByAnimalIdQueryHandler(AnnualSummaryQueryAdapter adapter)
    : DefaultQueryHandler<GetAnimalSummaryByAnimalIdQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetAnimalSummaryByAnimalIdQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}