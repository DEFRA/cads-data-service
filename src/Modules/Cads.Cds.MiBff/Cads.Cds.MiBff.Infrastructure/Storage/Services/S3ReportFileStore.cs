using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using System.Net;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Services;

public class S3ReportFileStore(IS3ClientFactory s3ClientFactory, IFileEncryptor fileEncryptor) : IReportFileStore
{
    private const string EncryptedContentType = "application/octet-stream";

    public async Task SaveAsync(string key, Stream content, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(content);

        await using var encrypted = new MemoryStream();

        await fileEncryptor.EncryptAsync(
            content,
            encrypted,
            totalBytes: content.CanSeek ? content.Length - content.Position : null,
            cancellationToken: cancellationToken);

        encrypted.Position = 0;

        await PutAsync(key, encrypted, cancellationToken);
    }

    public async Task<Stream> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        var response = await GetObjectAsync(key, cancellationToken);

        await using var encryptedStream = response.ResponseStream;
        var decrypted = new MemoryStream();

        await fileEncryptor.DecryptAsync(encryptedStream, decrypted, cancellationToken: cancellationToken);

        decrypted.Position = 0;
        return decrypted;
    }

    private async Task<GetObjectResponse> GetObjectAsync(string key, CancellationToken cancellationToken)
    {
        var client = s3ClientFactory.GetClientInfo<MiBffInternalStorageClient>();

        try
        {
            return await client.Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = client.BucketName,
                Key = key
            }, cancellationToken);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException("Report file", key);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.TooManyRequests)
        {
            throw new RetryableException($"Transient failure reading report at {key}.", ex);
        }
    }

    private async Task PutAsync(string key, Stream body, CancellationToken cancellationToken)
    {
        var client = s3ClientFactory.GetClientInfo<MiBffInternalStorageClient>();

        var request = new PutObjectRequest
        {
            BucketName = client.BucketName,
            Key = key,
            InputStream = body,
            ContentType = EncryptedContentType,
            AutoCloseStream = false
        };

        try
        {
            await client.Client.PutObjectAsync(request, cancellationToken);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.TooManyRequests)
        {
            throw new RetryableException($"Transient failure storing report at {key}.", ex);
        }
    }
}