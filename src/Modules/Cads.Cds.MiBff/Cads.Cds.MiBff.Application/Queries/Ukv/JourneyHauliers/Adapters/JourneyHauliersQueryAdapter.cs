using Cads.Cds.MiBff.Application.Queries.Ukv.JourneyHauliers;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.JourneyHauliers.Adapters;

public class JourneyHauliersQueryAdapter(IJourneyHaulierService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetJourneyHauliersQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}