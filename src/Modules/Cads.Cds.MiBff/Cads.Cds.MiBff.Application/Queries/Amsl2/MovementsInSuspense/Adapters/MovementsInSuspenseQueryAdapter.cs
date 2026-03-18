using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense.Adapters;

public class MovementsInSuspenseQueryAdapter(IMovementsInSuspenseService service)
{
    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetMovementsInSuspenseQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}