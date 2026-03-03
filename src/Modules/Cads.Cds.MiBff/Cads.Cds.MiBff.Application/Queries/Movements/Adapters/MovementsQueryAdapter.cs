using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Movements.Adapters;

public class MovementsQueryAdapter(IMovementService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}