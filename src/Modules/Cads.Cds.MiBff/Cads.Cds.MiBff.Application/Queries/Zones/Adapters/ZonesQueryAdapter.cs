using Cads.Cds.MiBff.Application.Services;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Zones.Adapters;

public class ZonesQueryAdapter(IZoneService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetZonesQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }

    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetZonesByZoneIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var results = await service.GetByZoneIdAsync(query.ZoneId, cancellationToken);

        return (results, results.Count());
    }
}