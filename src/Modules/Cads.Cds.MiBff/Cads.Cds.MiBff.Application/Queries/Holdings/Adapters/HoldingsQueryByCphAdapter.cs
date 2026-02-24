using Cads.Cds.MiBff.Application.Services;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;

public class HoldingsQueryByCphAdapter(IHoldingsService service)
{
    private readonly IHoldingsService _service = service;

    public async Task<(List<HoldingDTO> Items, int TotalCount)> GetHoldingsAsync(
        GetHoldingsByCphQuery query,
        CancellationToken cancellationToken = default)
    {
        var results = await _service.GetByCphAsync(query.cph, cancellationToken);

        return (
            results
            .Select(c => new HoldingDTO
            {
                Id = c.Id,
                Name = c.Name,
                Cph = c.Cph,
                LastModified = c.LastModified
            }).ToList(), results.Count());
    }
}