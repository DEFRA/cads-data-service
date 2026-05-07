using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Readers;

public class BulkImportStorageReader<T>(IS3ClientFactory s3ClientFactory)
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

    public Task<string> ReadAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> ListKeysAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var keys = new List<string>();

        if (CheckS3KeyTypeAsync(prefix).Result == "File")
        {
            keys.Add(prefix);
            return keys;
        }

        var request = new ListObjectsV2Request
        {
            BucketName = _bucketName,
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
    private async Task<string> CheckS3KeyTypeAsync(string key)
    {
        // 1. Check if exact object exists (file)
        try
        {
            await _s3Client.GetObjectMetadataAsync(_bucketName, key);
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

        var response = await _s3Client.ListObjectsV2Async(request);

        if (response?.S3Objects?.Count > 0)
        {
            return "Folder";
        }

        return "Not Found";
    }
}