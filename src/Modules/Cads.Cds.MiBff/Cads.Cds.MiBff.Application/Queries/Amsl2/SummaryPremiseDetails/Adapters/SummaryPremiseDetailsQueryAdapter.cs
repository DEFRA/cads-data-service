using Cads.Cds.MiBff.Core.DTOs.Amls2;
using Cads.Cds.MiBff.Core.Services.Amsl2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.SummaryPremiseDetails.Adapters;

public class SummaryPremiseDetailsQueryAdapter(ISummaryPremiseDetailsService service)
{
    public async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> GetAsync(
        GetSummaryPremiseDetailsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(cancellationToken);

        return (items, items.Count());
    }
}