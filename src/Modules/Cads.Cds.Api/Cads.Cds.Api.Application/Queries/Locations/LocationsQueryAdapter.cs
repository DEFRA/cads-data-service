using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class LocationsQueryAdapter(ILocationSummaryRepository locationSummaryRepository)
{
    public async Task<IEnumerable<LocationDto>> GetLocationsAsync(
        GetLocationsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await locationSummaryRepository.GetLocationSummaryAsync(query.Cph, query.LastModifiedDate, cancellationToken);

        return items.ToDtoList();
    }
}