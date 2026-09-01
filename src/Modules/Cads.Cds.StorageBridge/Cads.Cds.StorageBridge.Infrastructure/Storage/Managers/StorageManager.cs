using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Models;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Managers;

public class StorageManager<T>(IS3ClientFactory s3ClientFactory, IStorageReader<T> reader)
    : IStorageManager<T> where T : IStorageClient, new()
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<T>();

    public string BucketName { get; } = s3ClientFactory.GetClientBucketName<T>();
    public string ClientName { get; } = new T().ClientName;

    public Task<GetObjectResponse> GetObjectResponseAsync(string key, CancellationToken cancellationToken = default)
        => reader.GetObjectResponseAsync(key, cancellationToken);

    public Task<string> ReadAsync(string key, CancellationToken cancellationToken = default)
        => reader.ReadAsync(key, cancellationToken);

    public async Task<IEnumerable<string>> ListKeysAsync(string prefix, string? startAfterKey = null, CancellationToken cancellationToken = default)
    {
        var keys = new List<string>();

        await foreach (var s3Object in ListAllObjectsAsync(prefix, startAfterKey, cancellationToken))
        {
            keys.Add(s3Object.Key);
        }

        return keys;
    }

    public async Task<StorageObjectListing> ListObjectsAsync(string prefix, string? startAfterKey = null, string? delimiter = null, int maxKeys = 1000, string? continuationToken = null, CancellationToken cancellationToken = default)
    {
        var response = await _s3Client.ListObjectsV2Async(new ListObjectsV2Request
        {
            BucketName = BucketName,
            Prefix = prefix,
            StartAfter = startAfterKey,
            Delimiter = delimiter,
            MaxKeys = maxKeys,
            ContinuationToken = continuationToken
        }, cancellationToken);

        return new StorageObjectListing
        {
            Folders = (response.CommonPrefixes ?? []).Order().ToList(),
            Objects = (response.S3Objects ?? [])
                .Select(o => new StorageObjectItem(o.Key, o.Size ?? 0, o.LastModified, o.StorageClass?.Value))
                .OrderBy(o => o.Key, StringComparer.Ordinal)
                .ToList(),
            IsTruncated = response.IsTruncated.GetValueOrDefault(),
            NextContinuationToken = response.NextContinuationToken
        };
    }

    public async Task<IEnumerable<string>> SearchKeysAsync(string pattern, string? prefix = null, CancellationToken cancellationToken = default)
    {
        var keys = new List<string>();

        await foreach (var s3Object in ListAllObjectsAsync(prefix ?? string.Empty, cancellationToken: cancellationToken))
        {
            if (s3Object.Key.Contains(pattern, StringComparison.OrdinalIgnoreCase))
            {
                keys.Add(s3Object.Key);
            }
        }

        return keys;
    }

    public async Task PutObjectAsync(string key, Stream content, string? contentType = null, CancellationToken cancellationToken = default)
    {
        await _s3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = BucketName,
            Key = key,
            InputStream = content,
            ContentType = contentType ?? "application/octet-stream",
            AutoCloseStream = false
        }, cancellationToken);
    }

    public async Task DeleteObjectAsync(string key, CancellationToken cancellationToken = default)
    {
        await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = BucketName,
            Key = key
        }, cancellationToken);
    }

    private async IAsyncEnumerable<S3Object> ListAllObjectsAsync(string prefix, string? startAfterKey = null, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new ListObjectsV2Request
        {
            BucketName = BucketName,
            Prefix = prefix,
            StartAfter = startAfterKey
        };

        ListObjectsV2Response response;

        do
        {
            response = await _s3Client.ListObjectsV2Async(request, cancellationToken);

            foreach (var s3Object in response.S3Objects ?? [])
            {
                yield return s3Object;
            }

            request.ContinuationToken = response.NextContinuationToken;
        }
        while (response.IsTruncated.GetValueOrDefault());
    }
}