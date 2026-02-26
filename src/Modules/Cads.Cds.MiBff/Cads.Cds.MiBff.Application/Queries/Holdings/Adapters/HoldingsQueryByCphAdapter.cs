using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;

public class HoldingsQueryByCphAdapter(IHoldingsService service)
{
    private readonly IHoldingsService _service = service;

    public async Task<(IEnumerable<HoldingDTO> Items, int TotalCount)> GetHoldingsAsync(
        GetHoldingsByCphQuery query,
        CancellationToken cancellationToken = default)
    {
        var results = await _service.GetByCphAsync(query.cph, cancellationToken);

        return (results, results.Count());
    }
}