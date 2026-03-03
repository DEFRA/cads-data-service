using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.DataQuality.Adapters;

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