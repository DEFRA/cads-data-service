using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails.Adapters;

public class DestinationDetailsQueryAdapter(IDestinationDetailsService service)
{
    public async Task<(IEnumerable<DestinationDetailsDto> Items, int TotalCount)> GetAsync(
        GetDestinationDetailsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetByIdAndTypeAsync(query.DestinationId, query.DestinationType, cancellationToken);

        return (items, items.Count());
    }
}