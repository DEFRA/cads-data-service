namespace Cads.Cds.BuildingBlocks.Application.Requests;

public interface IPagedRequest
{
    int? Page { get; }

    int? PageSize { get; }

    string? Order { get; }

    string? Sort { get; }
}
