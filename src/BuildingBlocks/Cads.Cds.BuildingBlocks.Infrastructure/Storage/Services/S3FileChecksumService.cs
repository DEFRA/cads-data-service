using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using System.Security.Cryptography;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Services;

public class S3FileChecksumService<TClient>(IS3ClientFactory s3ClientFactory)
    : IFileChecksumService
    where TClient : IStorageClient, new()
{
    private readonly IAmazonS3 _s3Client = s3ClientFactory.GetClient<TClient>();
    private readonly string _bucketName = s3ClientFactory.GetClientBucketName<TClient>();

    public async Task<string> ComputeChecksumAsync(string location, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(location);

        var request = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = location
        };

        using var response = await _s3Client.GetObjectAsync(request, cancellationToken);
        await using var stream = response.ResponseStream;

        var hashBytes = await SHA256.HashDataAsync(stream, cancellationToken);
        return Convert.ToHexStringLower(hashBytes);
    }
}