namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Models;

public record StorageObjectListing
{
    public IReadOnlyList<string> Folders { get; init; } = [];

    public IReadOnlyList<StorageObjectItem> Objects { get; init; } = [];

    public bool IsTruncated { get; init; }

    public string? NextContinuationToken { get; init; }
}
