using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Models;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

public interface IStorageManager<T> : IStorageReader<T> where T : IStorageClient, new()
{
    string ClientName { get; }

    string BucketName { get; }

    Task<StorageObjectListing> ListObjectsAsync(string prefix, string? delimiter = null, int maxKeys = 1000, string? continuationToken = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> SearchKeysAsync(string pattern, string? prefix = null, CancellationToken cancellationToken = default);

    Task PutObjectAsync(string key, Stream content, string? contentType = null, CancellationToken cancellationToken = default);

    Task DeleteObjectAsync(string key, CancellationToken cancellationToken = default);
}
