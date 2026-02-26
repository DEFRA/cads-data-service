namespace Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

public class DefaultResult<T>
{
    public string? Message { get; set; }
    public string? Description { get; set; }
    public IEnumerable<T> Results { get; set; } = [];
}