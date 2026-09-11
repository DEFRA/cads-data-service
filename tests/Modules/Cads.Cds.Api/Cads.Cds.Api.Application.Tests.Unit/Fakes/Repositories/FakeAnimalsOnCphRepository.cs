using Cads.Cds.Api.Core.Domain.Paging;
using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Application.Tests.Unit.Fakes.Repositories;

public class FakeAnimalsOnCphRepository(params AnimalHoldingDto[] holdings) : IAnimalsOnCphRepository
{
    public Task<bool> HoldingExistsAsync(string cph, CancellationToken cancellationToken = default)
        => Task.FromResult(holdings.Any(h => Matches(h, cph)));

    public Task<PagedResult<TResult>> QueryAnimalsAsync<TResult>(
        string cph,
        Func<IQueryable<AnimalOnCphDto>, IQueryable<TResult>> shape,
        Func<IQueryable<TResult>, IOrderedQueryable<TResult>> order,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var animals = holdings
            .Where(h => Matches(h, cph))
            .SelectMany(h => h.Animals)
            .AsQueryable();

        var shaped = shape(animals);

        return Task.FromResult(new PagedResult<TResult>
        {
            TotalRecords = shaped.Count(),
            Items = [.. order(shaped).Skip((page - 1) * pageSize).Take(pageSize)]
        });
    }

    private static bool Matches(AnimalHoldingDto holding, string cph)
        => string.Equals(holding.Cph, cph, StringComparison.OrdinalIgnoreCase);
}