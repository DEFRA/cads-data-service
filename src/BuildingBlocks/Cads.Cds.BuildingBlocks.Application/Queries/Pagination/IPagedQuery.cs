namespace Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

public interface IPagedQuery<T> : IQuery<PaginatedResult<T>>
{
    int Page { get; set; }
    int PageSize { get; set; }

    /// <summary>
    /// Field to sort by
    /// </summary>
    string? Order { get; set; }

    /// <summary>
    /// "asc" or "desc"
    /// </summary>
    string? Sort { get; set; }
}