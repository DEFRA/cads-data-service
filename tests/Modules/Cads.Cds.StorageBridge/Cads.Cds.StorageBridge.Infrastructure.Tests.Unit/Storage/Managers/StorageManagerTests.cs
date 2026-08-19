using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Managers;
using FluentAssertions;
using Moq;
using System.Text;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Storage.Managers;

public class StorageManagerTests
{
    private const string BucketName = "test-bucket";
    private const string Key = "folder/report.csv";
    private const string ContentType = "text/csv";
    private const string Content = "id,name\n1,Cow";

    private readonly Mock<IAmazonS3> _s3 = new();
    private readonly Mock<IS3ClientFactory> _factory = new();
    private readonly Mock<IStorageReader<TestClient>> _reader = new();

    public class TestClient : IStorageClient
    {
        public string ClientName => GetType().Name;
    }

    public StorageManagerTests()
    {
        _factory.Setup(f => f.GetClient<TestClient>()).Returns(_s3.Object);
        _factory.Setup(f => f.GetClientBucketName<TestClient>()).Returns(BucketName);
    }

    [Fact]
    public async Task PutObjectAsync_ShouldCallPutObject_WithCorrectBucketKeyAndContentType()
    {
        _s3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new PutObjectResponse());

        using var content = new MemoryStream(Encoding.UTF8.GetBytes(Content));
        var sut = new StorageManager<TestClient>(_factory.Object, _reader.Object);

        await sut.PutObjectAsync(Key, content, ContentType, TestContext.Current.CancellationToken);

        _s3.Verify(s => s.PutObjectAsync(
            It.Is<PutObjectRequest>(r =>
                r.BucketName == BucketName &&
                r.Key == Key &&
                r.ContentType == ContentType &&
                r.InputStream == content),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task PutObjectAsync_ShouldDefaultContentTypeToOctetStream_WhenNoneGiven()
    {
        _s3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new PutObjectResponse());

        using var content = new MemoryStream(Encoding.UTF8.GetBytes(Content));
        var sut = new StorageManager<TestClient>(_factory.Object, _reader.Object);

        await sut.PutObjectAsync(Key, content, contentType: null, TestContext.Current.CancellationToken);

        _s3.Verify(s => s.PutObjectAsync(
            It.Is<PutObjectRequest>(r => r.ContentType == "application/octet-stream"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task PutObjectAsync_ShouldNotCloseTheCallersStream()
    {
        PutObjectRequest? captured = null;

        _s3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
           .Callback<PutObjectRequest, CancellationToken>((r, _) => captured = r)
           .ReturnsAsync(new PutObjectResponse());

        using var content = new MemoryStream(Encoding.UTF8.GetBytes(Content));
        var sut = new StorageManager<TestClient>(_factory.Object, _reader.Object);

        await sut.PutObjectAsync(Key, content, ContentType, TestContext.Current.CancellationToken);

        captured.Should().NotBeNull();
        captured!.AutoCloseStream.Should().BeFalse();
    }

    [Fact]
    public async Task PutObjectAsync_ShouldPropagate_WhenS3Throws()
    {
        _s3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
           .ThrowsAsync(new AmazonS3Exception("S3 unavailable"));

        using var content = new MemoryStream(Encoding.UTF8.GetBytes(Content));
        var sut = new StorageManager<TestClient>(_factory.Object, _reader.Object);

        await Assert.ThrowsAsync<AmazonS3Exception>(() =>
            sut.PutObjectAsync(Key, content, ContentType, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task DeleteObjectAsync_ShouldCallDeleteObject_WithCorrectBucketAndKey()
    {
        _s3.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new DeleteObjectResponse());

        var sut = new StorageManager<TestClient>(_factory.Object, _reader.Object);

        await sut.DeleteObjectAsync(Key, TestContext.Current.CancellationToken);

        _s3.Verify(s => s.DeleteObjectAsync(
            It.Is<DeleteObjectRequest>(r =>
                r.BucketName == BucketName &&
                r.Key == Key),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteObjectAsync_ShouldPropagate_WhenS3Throws()
    {
        _s3.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
           .ThrowsAsync(new AmazonS3Exception("Delete failed"));

        var sut = new StorageManager<TestClient>(_factory.Object, _reader.Object);

        await Assert.ThrowsAsync<AmazonS3Exception>(() =>
            sut.DeleteObjectAsync(Key, TestContext.Current.CancellationToken));
    }
}