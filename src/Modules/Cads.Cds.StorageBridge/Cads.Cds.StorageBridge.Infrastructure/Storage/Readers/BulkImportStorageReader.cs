using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Readers;

public sealed class BulkImportStorageReader<T>(IS3ClientFactory s3ClientFactory)
    : IStorageReader<T> where T : IStorageClient, new()
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<T>();
    private readonly string _bucketName = s3ClientFactory.GetClientBucketName<T>();

    public async Task<GetObjectResponse> GetObjectResponseAsync(string key, CancellationToken cancellationToken = default)
    {
        // Get the object from S3
        return await _s3Client.GetObjectAsync(new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        }, cancellationToken);
    }

    public async Task<string> ReadAsync(string key, CancellationToken cancellationToken = default)
    {
        using var response = await GetObjectResponseAsync(key, cancellationToken);
        using var reader = new StreamReader(response.ResponseStream);

        return await reader.ReadToEndAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> ListKeysAsync(string prefix, string? startAfterKey = null, CancellationToken cancellationToken = default)
    {
        var keys = new List<string>();

        if (await CheckS3KeyTypeAsync(prefix, cancellationToken) == "File")
        {
            keys.Add(prefix);
            return keys;
        }

        var request = new ListObjectsV2Request
        {
            BucketName = _bucketName,
            StartAfter = startAfterKey,
            Prefix = prefix.EndsWith('/') ? prefix : prefix + "/"
        };

        ListObjectsV2Response response;

        do
        {
            response = await _s3Client.ListObjectsV2Async(request, cancellationToken);
            keys.AddRange(response.S3Objects.Select(o => o.Key));
            request.ContinuationToken = response.NextContinuationToken;
        }
        while (response.IsTruncated.GetValueOrDefault());

        return keys;
    }

    /// <summary>
    /// Checks if the given key in S3 is a file, folder, or does not exist.
    /// </summary>
    private async Task<string> CheckS3KeyTypeAsync(string key, CancellationToken cancellationToken)
    {
        // 1. Check if exact object exists (file)
        try
        {
            await _s3Client.GetObjectMetadataAsync(_bucketName, key, cancellationToken);
            return "File";
        }
        catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Not a file, check if it's a folder
        }

        // 2. Check if any object exists with this key as a prefix (folder)
        var request = new ListObjectsV2Request
        {
            BucketName = _bucketName,
            Prefix = key.EndsWith('/') ? key : key + "/",
            MaxKeys = 1
        };

        var response = await _s3Client.ListObjectsV2Async(request, cancellationToken);

        if (response?.S3Objects?.Count > 0)
        {
            return "Folder";
        }

        return "Not Found";
    }
}