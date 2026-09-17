namespace Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

public sealed class PagingOptions
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 25;
}