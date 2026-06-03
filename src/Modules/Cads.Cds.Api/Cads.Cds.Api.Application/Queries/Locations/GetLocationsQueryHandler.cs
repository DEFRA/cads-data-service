using Cads.Cds.Api.Core.DTOs;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using MediatR;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQueryHandler(LocationsQueryAdapter adapter)
    : IRequestHandler<GetLocationsQuery, PaginatedResult<LocationDto>>
{
    public async Task<PaginatedResult<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await adapter.GetLocationsAsync(request, cancellationToken);

        return new PaginatedResult<LocationDto>
        {
            Count = items.Count,
            TotalCount = totalCount,
            Results = items,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}