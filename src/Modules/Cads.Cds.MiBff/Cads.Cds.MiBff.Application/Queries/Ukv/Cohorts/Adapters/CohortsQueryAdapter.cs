using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;
using Cads.Cds.MiBff.Core.Services.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts.Adapters;

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