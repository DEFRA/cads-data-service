using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.Ingester.Infrastructure.Services;

public class IngesterStorageWriter<T>(IS3ClientFactory s3ClientFactory) : IStorageWriter<T> where T : IStorageClient, new()
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<T>();
    private readonly string _bucketName = s3ClientFactory.GetClientBucketName<T>();

    public async Task WriteAsync(string key, string payload)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            ContentBody = payload
        };

        await _s3Client.PutObjectAsync(putObjectRequest);
    }
}