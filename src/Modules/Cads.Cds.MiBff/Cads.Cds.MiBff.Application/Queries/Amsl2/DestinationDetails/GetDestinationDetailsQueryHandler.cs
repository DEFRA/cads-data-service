using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails;

public class GetDestinationDetailsQueryHandler(DestinationDetailsQueryAdapter adapter)
    : DefaultQueryHandler<GetDestinationDetailsQuery, DestinationDetailsDto>
{
    protected override async Task<(IEnumerable<DestinationDetailsDto> Items, int TotalCount)> FetchAsync(GetDestinationDetailsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}