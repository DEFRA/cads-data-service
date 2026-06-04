using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Readers;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Services;

public class StorageService<T>(IS3ClientFactory s3ClientFactory)
    : BulkImportStorageReader<T>(s3ClientFactory), IStorageService<T>
    where T : IStorageClient, new()
{
    public async Task WriteAsync(string key, string payload, CancellationToken cancellationToken)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = BucketName,
            Key = key,
            ContentBody = payload
        };

        await S3Client.PutObjectAsync(putObjectRequest, cancellationToken);
    }

    public async Task CopyAsync(string sourceKey, string targetKey, CancellationToken cancellationToken)
    {
        await S3Client.CopyObjectAsync(new CopyObjectRequest
        {
            SourceBucket = BucketName,
            SourceKey = sourceKey,
            DestinationBucket = BucketName,
            DestinationKey = targetKey
        }, cancellationToken);

        await S3Client.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = BucketName,
            Key = sourceKey
        }, cancellationToken);
    }
}