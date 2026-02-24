namespace Cads.Cds.MiBff.Application.Queries.Pagination;

public class NonPaginatedResult<T>
{
    public int Count { get; set; }
    public List<T> Values { get; set; } = [];
}