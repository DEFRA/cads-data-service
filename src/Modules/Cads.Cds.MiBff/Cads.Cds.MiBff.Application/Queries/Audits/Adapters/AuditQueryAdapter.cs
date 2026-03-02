using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Audits.Adapters;

public class AuditQueryAdapter(IAuditService service)
{
    public async Task<(IEnumerable<UkvDto> Items, int TotalCount)> GetAsync(
        GetAuditScrapieQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetScrapieAsync(cancellationToken);

        return (items, items.Count());
    }
}