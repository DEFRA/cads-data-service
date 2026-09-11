using Cads.Cds.Api.Core.Domain.Bovine;
using Cads.Cds.Api.Core.DTOs.Bovine;
using System.Linq.Expressions;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public static class AnimalsOnCphSorting
{
    private static readonly Expression<Func<AnimalSummaryDto, string>> EarTagNumber =
        a => a.Identifier == null || a.Identifier.Identifier == null
            ? string.Empty
            : a.Identifier.Identifier.ToLower();

    private static readonly Expression<Func<AnimalSummaryDto, string>> Sex =
        a => a.Sex == null ? string.Empty : a.Sex.ToLower();

    private static readonly Expression<Func<AnimalSummaryDto, string>> BreedCode =
        a => a.BreedCode == null || a.BreedCode.Identifier == null
            ? string.Empty
            : a.BreedCode.Identifier.ToLower();

    public static IOrderedQueryable<AnimalSummaryDto> Apply(
        IQueryable<AnimalSummaryDto> animals,
        AnimalOrderBy orderBy,
        SortDirection direction)
        => orderBy switch
        {
            AnimalOrderBy.BirthDate => TieBreak(OrderBy(animals, a => a.BirthDate, direction), direction),
            AnimalOrderBy.DateOnCPH => TieBreak(OrderBy(animals, a => a.DateOnCph, direction), direction),
            AnimalOrderBy.Sex => TieBreak(OrderBy(animals, Sex, direction), direction),
            AnimalOrderBy.BreedCode => TieBreak(OrderBy(animals, BreedCode, direction), direction),
            _ => OrderBy(animals, EarTagNumber, direction)
        };

    private static IOrderedQueryable<AnimalSummaryDto> TieBreak(
        IOrderedQueryable<AnimalSummaryDto> animals,
        SortDirection direction)
        => direction == SortDirection.Desc
            ? animals.ThenByDescending(EarTagNumber)
            : animals.ThenBy(EarTagNumber);

    private static IOrderedQueryable<AnimalSummaryDto> OrderBy<TKey>(
        IQueryable<AnimalSummaryDto> animals,
        Expression<Func<AnimalSummaryDto, TKey>> keySelector,
        SortDirection direction)
        => direction == SortDirection.Desc
            ? animals.OrderByDescending(keySelector)
            : animals.OrderBy(keySelector);
}
