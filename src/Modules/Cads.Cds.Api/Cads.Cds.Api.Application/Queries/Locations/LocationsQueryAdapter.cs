using Cads.Cds.Api.Core.DTOs;

namespace Cads.Cds.Api.Application.Queries.Locations;

public class LocationsQueryAdapter
{
    /// <summary>
    /// No data layer yet so just returns empty list.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<(List<LocationDto> Items, int TotalCount)> GetLocationsAsync(
        GetLocationsQuery query,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = (new List<LocationDto>(), 0);

        return (items, totalCount);
    }
}