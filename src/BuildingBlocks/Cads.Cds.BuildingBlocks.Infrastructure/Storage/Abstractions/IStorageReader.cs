using Amazon.S3.Model;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

public interface IStorageReader<T>
{
    Task<GetObjectResponse> GetObjectResponseAsync(string key, CancellationToken cancellationToken = default);

    Task<string> ReadAsync(string key, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> ListKeysAsync(string prefix, CancellationToken cancellationToken = default);
}