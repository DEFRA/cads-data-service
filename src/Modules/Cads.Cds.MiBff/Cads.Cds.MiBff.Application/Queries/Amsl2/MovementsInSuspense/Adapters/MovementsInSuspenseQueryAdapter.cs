using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense.Adapters;

public class MovementsInSuspenseQueryAdapter(IMovementInSuspenseService service)
{
    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetMovementInSuspenseQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}