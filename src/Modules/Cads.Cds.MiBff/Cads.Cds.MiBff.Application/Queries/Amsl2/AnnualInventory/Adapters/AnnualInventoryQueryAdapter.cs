using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.AnnualInventory.Adapters;

public class AnnualInventoryQueryAdapter(IAnnualInventoryService service)
{
    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetAnnualInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}