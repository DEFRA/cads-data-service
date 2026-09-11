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
        if (!await repository.HoldingExistsAsync(query.Cph, cancellationToken))
        {
            return null;
        }

        var page = await repository.QueryAnimalsAsync<AnimalSummaryDto>(
            query.Cph,
            animals => AnimalsOnCphFilters.Apply(
                animals.Select(AnimalsOnCphProjections.ToSummary(query.HoldingAssociation)),
                query),
            animals => AnimalsOnCphSorting.Apply(animals, query.OrderBy, query.Direction),
            query.Page,
            query.PageSize,
            cancellationToken);

        return new AnimalCollectionDto
        {
            ResourceType = ResourceType,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = Math.Max(1, (int)Math.Ceiling((double)page.TotalRecords / query.PageSize)),
            TotalRecords = page.TotalRecords,
            Animals = page.Items
        };
    }
}