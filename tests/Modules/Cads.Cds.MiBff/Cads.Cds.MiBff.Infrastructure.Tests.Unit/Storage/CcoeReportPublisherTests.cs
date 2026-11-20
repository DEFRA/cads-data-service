using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using Cads.Cds.MiBff.Infrastructure.Storage.Configuration;
using Cads.Cds.MiBff.Infrastructure.Storage.Services;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Storage;

public class CcoeReportPublisherTests
{
    private const string BucketName = "defra-cads-reports-dev";
    private const string ReportKey = "gbcattledeaths";
    private const string ExpectedKey = $"reports/{ReportKey}/00000000-0000-0000-0000-000000000001/{ReportKey}.xlsx";
    private const string PresignedUrl = "https://ccoe.example.test/signed-url";

    private static readonly Guid JobId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    private readonly Mock<IAmazonS3> _s3Mock = new();
    private readonly S3ClientFactory _factory = new();

    private GetPreSignedUrlRequest? _presignRequest;
    private PutObjectRequest? _uploadedRequest;

    public CcoeReportPublisherTests()
    {
        _factory.RegisterMockClient<CcoeReportStorageClient>(BucketName, healthCheckEnabled: false, _s3Mock.Object);

        _s3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .Callback((PutObjectRequest request, CancellationToken _) => _uploadedRequest = request)
            .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

        _s3Mock
            .Setup(s => s.GetPreSignedURL(It.IsAny<GetPreSignedUrlRequest>()))
            .Callback((GetPreSignedUrlRequest request) => _presignRequest = request)
            .Returns(PresignedUrl);
    }

    private CcoeReportPublisher CreateSut(string kmsKeyArn = "", int expiryMinutes = 15) => new(
        _factory,
        new MiBffStorageConfiguration
        {
            Ccoe = new CcoeStorageConfigurationDetails
            {
                BucketName = BucketName,
                KmsKeyArn = kmsKeyArn,
                PresignedUrlExpiryMinutes = expiryMinutes
            }
        });

    private void SeedHeadNotFound() =>
        _s3Mock
            .Setup(s => s.GetObjectMetadataAsync(BucketName, ExpectedKey, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("missing") { StatusCode = HttpStatusCode.NotFound });

    private void SeedHeadFound() =>
        _s3Mock
            .Setup(s => s.GetObjectMetadataAsync(BucketName, ExpectedKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetObjectMetadataResponse());

    [Fact]
    public async Task PublishXlsxAsync_WhenObjectDoesNotExist_UploadsAndReturnsPresignedUrl()
    {
        SeedHeadNotFound();

        var result = await CreateSut().PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        result.Url.Should().Be(PresignedUrl);
        result.ExpiresAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddMinutes(15), TimeSpan.FromSeconds(5));

        _uploadedRequest.Should().NotBeNull();
        _uploadedRequest!.BucketName.Should().Be(BucketName);
        _uploadedRequest.Key.Should().Be(ExpectedKey);
        _uploadedRequest.ContentType.Should().Be("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        _uploadedRequest.Headers.ContentDisposition.Should().Be($"attachment; filename=\"{ReportKey}.xlsx\"");

        _presignRequest.Should().NotBeNull();
        _presignRequest!.BucketName.Should().Be(BucketName);
        _presignRequest.Key.Should().Be(ExpectedKey);
        _presignRequest.Verb.Should().Be(HttpVerb.GET);
    }

    [Fact]
    public async Task PublishXlsxAsync_WhenObjectAlreadyExists_SkipsUploadAndReturnsPresignedUrl()
    {
        SeedHeadFound();

        var result = await CreateSut().PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        result.Url.Should().Be(PresignedUrl);
        _s3Mock.Verify(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task PublishXlsxAsync_WhenKmsKeyConfigured_SetsServerSideEncryption()
    {
        SeedHeadNotFound();
        const string kmsKeyArn = "arn:aws:kms:eu-west-2:747301448395:key/50341804-6c4f-example";

        await CreateSut(kmsKeyArn).PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        _uploadedRequest.Should().NotBeNull();
        _uploadedRequest!.ServerSideEncryptionMethod.Should().Be(ServerSideEncryptionMethod.AWSKMS);
        _uploadedRequest.ServerSideEncryptionKeyManagementServiceKeyId.Should().Be(kmsKeyArn);
    }

    [Fact]
    public async Task PublishXlsxAsync_WhenNoKmsKeyConfigured_OmitsServerSideEncryption()
    {
        SeedHeadNotFound();

        await CreateSut(kmsKeyArn: "").PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        _uploadedRequest.Should().NotBeNull();
        _uploadedRequest!.ServerSideEncryptionMethod.Should().BeNull();
    }

    [Fact]
    public async Task PublishXlsxAsync_UsesConfiguredExpiryMinutes()
    {
        SeedHeadNotFound();

        var result = await CreateSut(expiryMinutes: 30).PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        result.ExpiresAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddMinutes(30), TimeSpan.FromSeconds(5));
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task PublishXlsxAsync_WhenHeadCheckFailsTransiently_ThrowsRetryable(HttpStatusCode statusCode)
    {
        _s3Mock
            .Setup(s => s.GetObjectMetadataAsync(BucketName, ExpectedKey, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("unavailable") { StatusCode = statusCode });

        var act = () => CreateSut().PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<RetryableException>().WithInnerException(typeof(AmazonS3Exception));
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task PublishXlsxAsync_WhenUploadFailsTransiently_ThrowsRetryable(HttpStatusCode statusCode)
    {
        SeedHeadNotFound();
        _s3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("unavailable") { StatusCode = statusCode });

        var act = () => CreateSut().PublishXlsxAsync(
            ReportKey, JobId, new MemoryStream([1, 2, 3]), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<RetryableException>().WithInnerException(typeof(AmazonS3Exception));
    }
}