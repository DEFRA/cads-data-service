namespace Cads.Cds.Api.Core.Domain.Paging;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];

    public int TotalRecords { get; init; }
}