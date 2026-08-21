namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Models;

public record StorageObjectItem(string Key, long Size, DateTime? LastModified, string? StorageClass);