using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements.Adapters;

public class DetailedMovementsQueryAdapter(IDetailedMovementsService service)
{
    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetDetailedMovementsByMovementIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByMovementIdAsync(query.MovementId, cancellationToken);

        return (items, items.Count());
    }

    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetDetailedMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}