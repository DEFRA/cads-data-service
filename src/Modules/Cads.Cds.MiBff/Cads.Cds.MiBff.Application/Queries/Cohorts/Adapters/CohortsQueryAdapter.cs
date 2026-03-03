using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Cohorts.Adapters;

public class CohortsQueryAdapter(ICohortService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetCohortsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }

    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetCohortsByAnimalIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByAnimalIdAsync(query.AnimalId, cancellationToken);

        return (items, items.Count());
    }
}