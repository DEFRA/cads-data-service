using Cads.Cds.MiBff.Core.DTOs.Ukv;
using Cads.Cds.MiBff.Core.Services.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Holdings.Adapters;

public class HoldingsQueryAdapter(IHoldingService service)
{
    public async Task<(IEnumerable<HoldingDto> Items, int TotalCount)> GetAsync(
        GetHoldingsByCphQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByCphAsync(query.cph, cancellationToken);

        return (items, items.Count());
    }

    public async Task<(IEnumerable<HoldingDto> Items, int TotalCount)> GetAsync(
        GetHoldingsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}