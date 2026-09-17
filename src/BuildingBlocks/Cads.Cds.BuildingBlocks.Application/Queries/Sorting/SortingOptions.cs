namespace Cads.Cds.BuildingBlocks.Application.Queries.Sorting;

public sealed class SortingOptions<TOrderBy> where TOrderBy : Enum
{
    public TOrderBy OrderBy { get; init; } = default!;
    public SortDirection Direction { get; init; } = SortDirection.Asc;
}