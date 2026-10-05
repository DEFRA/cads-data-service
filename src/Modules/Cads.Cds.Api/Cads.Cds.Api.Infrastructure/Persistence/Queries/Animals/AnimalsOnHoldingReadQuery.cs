using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;
using Cads.Cds.Api.Core.Domain.Animals;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Application.Queries.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Infrastructure.Persistence.Queries.Animals;

public class AnimalsOnHoldingReadQuery(ApiReadDbContext dbContext) : IAnimalsOnHoldingReadQuery
{
    private const int MaxPageSize = 100;

    public async Task<AnimalsOnHoldingResult> ExecuteAsync(
        GetAnimalsOnHolding query,
        CancellationToken cancellationToken = default)
    {
        var page = Math.Max(query.Paging.Page, 1);
        var pageSize = Math.Clamp(query.Paging.PageSize, 1, MaxPageSize);

        var rowFrom = (long)(page - 1) * pageSize + 1;
        var rowTo = (long)page * pageSize;

        var items = await dbContext
            .GetAnimalsOnHolding(
                query.Cph,
                query.IncludeHistorical,
                rowFrom,
                rowTo,
                ToSortField(query.Sorting.OrderBy),
                query.Sorting.Direction == SortDirection.Desc
                    ? "desc"
                    : "asc",
                query.Filters.BreedCode?.Trim().ToUpperInvariant(),
                query.Filters.Sex.HasValue
                    ? ToSexCode(query.Filters.Sex.Value)
                    : null)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new AnimalsOnHoldingResult(
            items,
            items.Count > 0 ? items[0].TotalCount : 0,
            page,
            pageSize);
    }

    private static string ToSortField(AnimalsOnHoldingOrderBy orderBy) => orderBy switch
    {
        AnimalsOnHoldingOrderBy.BirthDate => "date_of_birth",
        AnimalsOnHoldingOrderBy.DateOnCPH => "date_on_cph",
        AnimalsOnHoldingOrderBy.Sex => "sex",
        AnimalsOnHoldingOrderBy.BreedCode => "breed_code",
        _ => "ear_tag_number"
    };

    private static string ToSexCode(AnimalSex sex) => sex switch
    {
        AnimalSex.Female => "F",
        AnimalSex.Male => "M",
        _ => throw new ArgumentOutOfRangeException(nameof(sex), sex, null)
    };
}
