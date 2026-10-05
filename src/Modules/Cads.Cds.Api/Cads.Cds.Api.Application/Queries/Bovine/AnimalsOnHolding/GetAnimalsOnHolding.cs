using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.BuildingBlocks.Application.Queries.Sorting;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public class GetAnimalsOnHolding : IQuery<AnimalsOnHoldingDto>
{
    public required string Cph { get; init; }
    public bool IncludeHistorical { get; init; } = false;

    public AnimalsOnHoldingFilters Filters { get; init; } = new();
    public PagingOptions Paging { get; init; } = new();
    public SortingOptions<AnimalsOnHoldingOrderBy> Sorting { get; init; } = new();
}