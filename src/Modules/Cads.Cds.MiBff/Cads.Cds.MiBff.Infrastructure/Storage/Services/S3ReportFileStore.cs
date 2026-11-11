using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using System.Net;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Services;

/// <summary>
/// Encrypts report output with <see cref="IFileEncryptor"/> and writes it to the CADS internal bucket.
/// </summary>
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
