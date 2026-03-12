using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails;

public class GetDepartureDetailsByCphQueryHandler(DepartureDetailsQueryAdapter adapter)
    : JsonResponseDataQueryHandler<GetDepartureDetailsByCphQuery, DepartureDetailsDto>
{
    protected override async Task<(IEnumerable<DepartureDetailsDto> Items, int TotalCount)> FetchAsync(GetDepartureDetailsByCphQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}