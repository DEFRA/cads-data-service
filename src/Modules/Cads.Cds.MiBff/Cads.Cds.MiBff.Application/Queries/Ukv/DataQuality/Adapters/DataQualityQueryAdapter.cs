using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;
using Cads.Cds.MiBff.Core.Services.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.DataQuality.Adapters;

public class DataQualityQueryAdapter(IDataQualityService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetDataQualityUnregisteredQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetUnregisteredAsync(cancellationToken);

        return (items, items.Count());
    }

}