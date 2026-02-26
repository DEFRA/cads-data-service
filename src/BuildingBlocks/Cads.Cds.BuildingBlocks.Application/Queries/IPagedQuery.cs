using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

namespace Cads.Cds.BuildingBlocks.Application.Queries;

public interface IPagedQuery<T> : IQuery<PaginatedResult<T>>
{
    int Page { get; }
    int PageSize { get; }

    /// <summary>
    /// Field to sort by
    /// </summary>
    string? Order { get; }

    /// <summary>
    /// "asc" or "desc"
    /// </summary>
    string? Sort { get; }
}