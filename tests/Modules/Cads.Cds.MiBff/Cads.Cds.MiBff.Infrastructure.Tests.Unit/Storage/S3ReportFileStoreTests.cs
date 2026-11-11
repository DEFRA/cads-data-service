using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using Cads.Cds.MiBff.Infrastructure.Storage.Services;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Storage;

public class S3ReportFileStoreTests
{
    private const string BucketName = "cads-internal-bucket";
    private const string Key = "mibff-reports/gbcattledeaths/00000000-0000-0000-0000-000000000001";

    private readonly Mock<IAmazonS3> _s3Mock = new();
    private readonly Mock<IFileEncryptor> _encryptorMock = new();
    private readonly S3ClientFactory _factory = new();

    private byte[]? _uploadedBytes;
    private PutObjectRequest? _uploadedRequest;

    public S3ReportFileStoreTests()
    {
        _factory.RegisterMockClient<MiBffInternalStorageClient>(BucketName, healthCheckEnabled: false, _s3Mock.Object);

        // Fake "encryption": prefix the plaintext with a marker so the test can tell it went through the encryptor.
        _encryptorMock
            .Setup(e => e.EncryptAsync(It.IsAny<Stream>(), It.IsAny<Stream>(), It.IsAny<long?>(),
                It.IsAny<Action<int, string>?>(), It.IsAny<CancellationToken>()))
            .Returns(async (Stream input, Stream output, long? _, Action<int, string>? _, CancellationToken ct) =>
            {
                await output.WriteAsync("ENC:"u8.ToArray(), ct);
                await input.CopyToAsync(output, ct);
            });

        _s3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .Callback((PutObjectRequest request, CancellationToken _) =>
            {
                _uploadedRequest = request;
                using var copy = new MemoryStream();
                request.InputStream.CopyTo(copy);
                _uploadedBytes = copy.ToArray();
            })
            .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });
    }

    private S3ReportFileStore CreateSut() => new(_factory, _encryptorMock.Object);

    [Fact]
    public async Task SaveAsync_UploadsEncryptedContentToInternalBucketUnderKey()
    {
        using var content = new MemoryStream("[{\"a\":1}]"u8.ToArray());

        await CreateSut().SaveAsync(Key, content, TestContext.Current.CancellationToken);

        _uploadedRequest.Should().NotBeNull();
        _uploadedRequest!.BucketName.Should().Be(BucketName);
        _uploadedRequest.Key.Should().Be(Key);
        _uploadedRequest.ContentType.Should().Be("application/octet-stream");
        _uploadedBytes.Should().Equal("ENC:[{\"a\":1}]"u8.ToArray());
    }

    [Fact]
    public async Task SaveAsync_PassesRemainingLengthToEncryptor()
    {
        using var content = new MemoryStream(new byte[10]) { Position = 4 };

        await CreateSut().SaveAsync(Key, content, TestContext.Current.CancellationToken);

        _encryptorMock.Verify(e => e.EncryptAsync(content, It.IsAny<Stream>(), 6L,
            It.IsAny<Action<int, string>?>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SaveAsync_DoesNotCloseCallerStream()
    {
        using var content = new MemoryStream([1, 2, 3]);

        await CreateSut().SaveAsync(Key, content, TestContext.Current.CancellationToken);

        content.CanRead.Should().BeTrue();
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task SaveAsync_WhenS3FailsTransiently_ThrowsRetryable(HttpStatusCode statusCode)
    {
        _s3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("unavailable") { StatusCode = statusCode });

        var act = () => CreateSut().SaveAsync(Key, new MemoryStream([1]), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<RetryableException>().WithInnerException(typeof(AmazonS3Exception));
    }

    [Fact]
    public async Task SaveAsync_WhenS3RejectsRequest_PropagatesOriginalException()
    {
        _s3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("denied") { StatusCode = HttpStatusCode.Forbidden });

        var act = () => CreateSut().SaveAsync(Key, new MemoryStream([1]), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<AmazonS3Exception>();
    }

    [Fact]
    public async Task SaveAsync_WhenEncryptionFails_DoesNotUpload()
    {
        _encryptorMock
            .Setup(e => e.EncryptAsync(It.IsAny<Stream>(), It.IsAny<Stream>(), It.IsAny<long?>(),
                It.IsAny<Action<int, string>?>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("no key"));

        var act = () => CreateSut().SaveAsync(Key, new MemoryStream([1]), TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<InvalidOperationException>();
        _s3Mock.Verify(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}