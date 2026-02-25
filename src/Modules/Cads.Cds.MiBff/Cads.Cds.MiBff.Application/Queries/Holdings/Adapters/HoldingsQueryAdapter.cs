using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;

public class HoldingsQueryAdapter(IHoldingsService service)
{
    private readonly IHoldingsService _service = service;

    public async Task<(IEnumerable<HoldingDTO> Items, int TotalCount)> GetHoldingsAsync(
        GetHoldingsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await _service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}