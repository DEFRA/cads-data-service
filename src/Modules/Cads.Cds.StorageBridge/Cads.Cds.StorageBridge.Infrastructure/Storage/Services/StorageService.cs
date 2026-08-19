using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Application.S3Import.Services;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Services;

public class StorageService<T>(IS3ClientFactory s3ClientFactory, IStorageReader<T> reader)
    : IStorageService<T> where T : IStorageClient, new()
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<T>();
    private readonly string _bucketName = s3ClientFactory.GetClientBucketName<T>();

    public Task<GetObjectResponse> GetObjectResponseAsync(string key, CancellationToken cancellationToken = default)
        => reader.GetObjectResponseAsync(key, cancellationToken);

    public Task<string> ReadAsync(string key, CancellationToken cancellationToken = default)
        => reader.ReadAsync(key, cancellationToken);

    public Task<IEnumerable<string>> ListKeysAsync(string prefix, CancellationToken cancellationToken = default)
        => reader.ListKeysAsync(prefix, cancellationToken);

    public async Task WriteAsync(string key, string payload, CancellationToken cancellationToken)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            ContentBody = payload
        };

        await _s3Client.PutObjectAsync(putObjectRequest, cancellationToken);
    }

    public async Task CopyAsync(string sourceKey, string targetKey, CancellationToken cancellationToken)
    {
        await _s3Client.CopyObjectAsync(new CopyObjectRequest
        {
            SourceBucket = _bucketName,
            SourceKey = sourceKey,
            DestinationBucket = _bucketName,
            DestinationKey = targetKey
        }, cancellationToken);

        await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = sourceKey
        }, cancellationToken);
    }
}