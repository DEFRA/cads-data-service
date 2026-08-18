using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Models;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Managers;

public class StorageManager<T>(IS3ClientFactory s3ClientFactory)
    : IStorageManager<T> where T : IStorageClient, new()
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<T>();
    
    public string BucketName { get; } = s3ClientFactory.GetClientBucketName<T>();
    public string ClientName { get; } = new T().ClientName;

    public async Task<GetObjectResponse> GetObjectResponseAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _s3Client.GetObjectAsync(new GetObjectRequest
        {
            BucketName = BucketName,
            Key = key
        }, cancellationToken);
    }

    public async Task<string> ReadAsync(string key, CancellationToken cancellationToken = default)
    {
        using var response = await GetObjectResponseAsync(key, cancellationToken);
        using var reader = new StreamReader(response.ResponseStream);

        return await reader.ReadToEndAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> ListKeysAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var keys = new List<string>();

        await foreach (var s3Object in ListAllObjectsAsync(prefix, cancellationToken))
        {
            keys.Add(s3Object.Key);
        }

        return keys;
    }

    public async Task<StorageObjectListing> ListObjectsAsync(string prefix, string? delimiter = null, int maxKeys = 1000, string? continuationToken = null, CancellationToken cancellationToken = default)
    {
        var response = await _s3Client.ListObjectsV2Async(new ListObjectsV2Request
        {
            BucketName = BucketName,
            Prefix = prefix,
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

        await foreach (var s3Object in ListAllObjectsAsync(prefix ?? string.Empty, cancellationToken))
        {
            if (s3Object.Key.Contains(pattern, StringComparison.OrdinalIgnoreCase))
            {
                keys.Add(s3Object.Key);
            }
        }

        return keys;
    }

    private async IAsyncEnumerable<S3Object> ListAllObjectsAsync(string prefix, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var request = new ListObjectsV2Request
        {
            BucketName = BucketName,
            Prefix = prefix
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
