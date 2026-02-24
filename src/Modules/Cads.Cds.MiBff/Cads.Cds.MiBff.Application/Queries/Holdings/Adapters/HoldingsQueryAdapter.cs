using Cads.Cds.MiBff.Application.Services;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;

public class HoldingsQueryAdapter(IHoldingsService service)
{
    private readonly IHoldingsService _service = service;

    public async Task<(List<HoldingDTO> Items, int TotalCount)> GetHoldingsAsync(
        GetHoldingsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await _service.GetAllAsync(cancellationToken);

        var results = items
            .Where(c => !query.LastModified.HasValue || c.LastModified >= query.LastModified.Value)
            .Where(c => string.IsNullOrEmpty(query.Name) || c.Name.Contains(query.Name, StringComparison.InvariantCultureIgnoreCase));
           
        var sortedResults = query.Order switch
        {
            "name" => query.Sort != "desc" ? results.OrderBy(c => c.Name) : results.OrderByDescending(c => c.Name),
            _ => results.OrderBy(c => c.Id)
        };

        return (
            sortedResults
            .Select(c => new HoldingDTO
            {
                Id = c.Id,
                Name = c.Name,
                Cph = c.Cph,
                LastModified = c.LastModified
            }).Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList(), items.Count());
    }
}