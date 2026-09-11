using Cads.Cds.Api.Core.Domain.Bovine;
using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public class AnimalsOnCphQueryAdapter(IAnimalsOnCphRepository repository)
{
    public const string ResourceType = "AnimalCollection";

    public async Task<AnimalCollectionDto?> GetAsync(
        GetAnimalsOnCph query,
        CancellationToken cancellationToken = default)
    {
        var holding = await repository.GetByCphAsync(query.Cph, cancellationToken);

        if (holding is null)
        {
            return null;
        }

        var matching = holding.Animals
            .ToSummaryDtoList(query.HoldingAssociation)
            .Where(a => a.DateOnCph is not null)
            .Where(a => MatchesFilters(a, query))
            .Where(a => MatchesSearchTerm(a, query.Q))
            .ToList();

        var totalRecords = matching.Count;

        return new AnimalCollectionDto
        {
            ResourceType = ResourceType,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / query.PageSize)),
            TotalRecords = totalRecords,
            Animals = Sort(matching, query.OrderBy, query.Direction)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList()
        };
    }

    private static bool MatchesFilters(AnimalSummaryDto animal, GetAnimalsOnCph query)
    {
        if (query.Status.Any() && !query.Status.Any(s => Matches(animal.Status, s.ToString())))
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(query.Sex) && !Matches(animal.Sex, query.Sex))
        {
            return false;
        }

        if (query.BreedCode.Any() && !query.BreedCode.Any(b => Matches(animal.BreedCode?.Identifier, b)))
        {
            return false;
        }

        return query.DateOnCphFrom is null || animal.DateOnCph >= query.DateOnCphFrom;
    }

    private static bool MatchesSearchTerm(AnimalSummaryDto animal, string? term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return true;
        }

        return Contains(animal.Identifier?.Identifier, term)
            || Matches(animal.Sex, term)
            || Matches(animal.BreedCode?.Identifier, term)
            || Contains(animal.BreedCode?.BreedName, term);
    }

    private static bool Matches(string? value, string term)
        => string.Equals(value, term, StringComparison.OrdinalIgnoreCase);

    private static bool Contains(string? value, string term)
        => value is not null && value.Contains(term, StringComparison.OrdinalIgnoreCase);

    private static IEnumerable<AnimalSummaryDto> Sort(
        IEnumerable<AnimalSummaryDto> animals,
        AnimalOrderBy orderBy,
        SortDirection direction)
        => orderBy switch
        {
            AnimalOrderBy.BirthDate => Order(animals, a => a.BirthDate, direction),
            AnimalOrderBy.DateOnCPH => Order(animals, a => a.DateOnCph, direction),
            AnimalOrderBy.Sex => Order(animals, a => a.Sex, direction, StringComparer.OrdinalIgnoreCase),
            AnimalOrderBy.BreedCode => Order(animals, a => a.BreedCode?.Identifier, direction, StringComparer.OrdinalIgnoreCase),
            _ => Order(animals, a => a.Identifier?.Identifier, direction, StringComparer.OrdinalIgnoreCase)
        };

    private static IEnumerable<AnimalSummaryDto> Order<TKey>(
        IEnumerable<AnimalSummaryDto> animals,
        Func<AnimalSummaryDto, TKey> keySelector,
        SortDirection direction,
        IComparer<TKey>? comparer = null)
        => direction == SortDirection.Desc
            ? animals.OrderByDescending(keySelector, comparer)
            : animals.OrderBy(keySelector, comparer);
}