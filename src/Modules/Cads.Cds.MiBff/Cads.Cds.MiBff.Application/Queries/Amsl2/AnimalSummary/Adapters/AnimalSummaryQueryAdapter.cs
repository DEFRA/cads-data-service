using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary.Adapters;

public class AnnualSummaryQueryAdapter(IAnimalSummaryService service)
{
    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetAnimalSummaryByAnimalIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByAnimalIdAsync(query.AnimalId, cancellationToken);

        return (items, items.Count());
    }
}