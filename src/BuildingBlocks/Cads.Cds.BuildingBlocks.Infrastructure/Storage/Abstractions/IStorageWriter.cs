namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

public interface IStorageWriter<T> where T : IStorageClient, new()
{
    Task WriteAsync(string key, string payload, CancellationToken cancellationToken);

    Task WriteAsync(string key, Stream fileStream, CancellationToken cancellationToken);

    Task CopyAsync(string sourceKey, string targetKey, CancellationToken cancellationToken);
}