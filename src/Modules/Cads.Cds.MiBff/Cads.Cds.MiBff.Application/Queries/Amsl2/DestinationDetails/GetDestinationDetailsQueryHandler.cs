using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails.Adapters;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails;

public class GetDestinationDetailsQueryHandler(DestinationDetailsQueryAdapter adapter)
    : JsonResponseDataQueryHandler<GetDestinationDetailsQuery, DestinationDetailsDto>
{
    protected override async Task<(IEnumerable<DestinationDetailsDto> Items, int TotalCount)> FetchAsync(GetDestinationDetailsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}