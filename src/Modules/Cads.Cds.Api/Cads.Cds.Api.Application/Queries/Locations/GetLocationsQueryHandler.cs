using Cads.Cds.Api.Core.DTOs;
using MediatR;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class GetLocationsQueryHandler(LocationsQueryAdapter adapter)
    : IRequestHandler<GetLocationsQuery, IEnumerable<LocationDto>>
{
    public async Task<IEnumerable<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        return await adapter.GetLocationsAsync(request, cancellationToken);
    }
}