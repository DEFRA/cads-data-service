using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails;

public class GetDepartureDetailsByCphQueryHandler(DepartureDetailsQueryAdapter adapter)
    : DefaultQueryHandler<GetDepartureDetailsByCphQuery, DepartureDetailsDto>
{
    protected override async Task<(IEnumerable<DepartureDetailsDto> Items, int TotalCount)> FetchAsync(GetDepartureDetailsByCphQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}