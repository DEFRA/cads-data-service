using Cads.Cds.MiBff.Core.DTOs.Ukv;
using Cads.Cds.MiBff.Core.Services.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Inspections.Adapters;

public class InspectionsQueryAdapter(IInspectionService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetInspectionsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}