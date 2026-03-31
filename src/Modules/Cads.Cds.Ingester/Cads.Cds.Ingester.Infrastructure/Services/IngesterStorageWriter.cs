using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Core.Storage;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.Ingester.Infrastructure.Storage.Clients;

namespace Cads.Cds.Ingester.Infrastructure.Services;

public class IngesterStorageWriter(IS3ClientFactory s3ClientFactory) : IStorageWriter
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<CadsIngesterClient>();
    private readonly string _bucketName = s3ClientFactory.GetClientBucketName<CadsIngesterClient>();

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