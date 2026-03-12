using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Amsl2.SummaryPremiseDetails.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.SummaryPremiseDetails;

public class GetSummaryPremiseDetailsQueryHandler(SummaryPremiseDetailsQueryAdapter adapter)
    : PagedQueryHandler<GetSummaryPremiseDetailsQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetSummaryPremiseDetailsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}