using Cads.Cds.MiBff.Application.Services;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Animals.Adapters;

public class AnimalsQueryAdapter(IAnimalService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetAnimalsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }

    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetAnimalsByAnimalIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByAnimalIdAsync(query.AnimalId, cancellationToken);

        return (items, items.Count());
    }
}