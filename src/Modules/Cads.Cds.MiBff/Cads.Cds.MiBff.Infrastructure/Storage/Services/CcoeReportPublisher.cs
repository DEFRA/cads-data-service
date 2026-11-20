using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using Cads.Cds.MiBff.Infrastructure.Storage.Configuration;
using System.Net;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Services;

public class CcoeReportPublisher(IS3ClientFactory s3ClientFactory, MiBffStorageConfiguration storageConfig)
    : ICcoeReportPublisher
{
    private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public async Task<ReportDownloadLink> PublishXlsxAsync(
        string reportKey, Guid jobId, MemoryStream xlsxContent, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reportKey);
        ArgumentNullException.ThrowIfNull(xlsxContent);

        var client = s3ClientFactory.GetClientInfo<CcoeReportStorageClient>();
        var key = BuildKey(reportKey, jobId);

        if (!await ExistsAsync(client.Client, client.BucketName, key, cancellationToken))
        {
            await UploadAsync(client.Client, client.BucketName, key, reportKey, xlsxContent, cancellationToken);
        }

        return Presign(client.Client, client.BucketName, key);
    }

    private static string BuildKey(string reportKey, Guid jobId) => $"reports/{reportKey}/{jobId}/{reportKey}.xlsx";

    private static async Task<bool> ExistsAsync(IAmazonS3 client, string bucket, string key, CancellationToken cancellationToken)
    {
        try
        {
            await client.GetObjectMetadataAsync(bucket, key, cancellationToken);
            return true;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.TooManyRequests)
        {
            throw new RetryableException($"Transient failure checking for report at {key}.", ex);
        }
    }

    private async Task UploadAsync(
        IAmazonS3 client, string bucket, string key, string reportKey, MemoryStream xlsxContent, CancellationToken cancellationToken)
    {
        xlsxContent.Position = 0;

        var request = new PutObjectRequest
        {
            BucketName = bucket,
            Key = key,
            InputStream = xlsxContent,
            AutoCloseStream = false,
            ContentType = XlsxContentType
        };

        request.Headers.ContentDisposition = $"attachment; filename=\"{reportKey}.xlsx\"";

        if (!string.IsNullOrWhiteSpace(storageConfig.Ccoe.KmsKeyArn))
        {
            request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AWSKMS;
            request.ServerSideEncryptionKeyManagementServiceKeyId = storageConfig.Ccoe.KmsKeyArn;
        }

        try
        {
            await client.PutObjectAsync(request, cancellationToken);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.TooManyRequests)
        {
            throw new RetryableException($"Transient failure storing report at {key}.", ex);
        }
    }

    private ReportDownloadLink Presign(IAmazonS3 client, string bucket, string key)
    {
        var expiryMinutes = storageConfig.Ccoe.PresignedUrlExpiryMinutes > 0
            ? storageConfig.Ccoe.PresignedUrlExpiryMinutes
            : 15;

        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(expiryMinutes);

        var url = client.GetPreSignedURL(new GetPreSignedUrlRequest
        {
            BucketName = bucket,
            Key = key,
            Verb = HttpVerb.GET,
            Expires = expiresAt.UtcDateTime
        });

        return new ReportDownloadLink(url, expiresAt);
    }
}