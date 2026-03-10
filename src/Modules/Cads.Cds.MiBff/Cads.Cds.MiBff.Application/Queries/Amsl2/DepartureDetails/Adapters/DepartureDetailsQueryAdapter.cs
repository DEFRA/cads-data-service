using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails.Adapters;

public class DepartureDetailsQueryAdapter(IDepartureDetailsService service)
{
    public async Task<(IEnumerable<DepartureDetailsDto> Items, int TotalCount)> GetAsync(
        GetDepartureDetailsByCphQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByCphAsync(query.Cph, cancellationToken);

        return (items, items.Count());
    }
}