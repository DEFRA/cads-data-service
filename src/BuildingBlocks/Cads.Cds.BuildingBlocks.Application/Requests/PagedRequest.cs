namespace Cads.Cds.BuildingBlocks.Application.Responses;

public class PagedRequest
{
    public int Page { get; }

    public int PageSize { get; }

    public string? Order { get; }

    public string? Sort { get; }
}
