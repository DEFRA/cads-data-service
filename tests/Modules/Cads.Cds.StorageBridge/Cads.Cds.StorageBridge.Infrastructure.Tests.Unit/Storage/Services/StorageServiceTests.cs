using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Services;
using FluentAssertions;
using Moq;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Storage.Services;

public class StorageServiceTests
{
    private const string BucketName = "test-bucket";
    private const string SourceKey = "sql-scripts/insert_animals.sql";
    private const string TargetKey = "data-seed/file-error/insert_animals.sql";
    private const string Payload = "INSERT INTO animals VALUES (1, 'Cow');";

    private readonly Mock<IAmazonS3> _s3 = new();
    private readonly Mock<IS3ClientFactory> _factory = new();

    private class TestClient : IStorageClient
    {
        public string ClientName => GetType().Name;
    }

    public StorageServiceTests()
    {
        _factory.Setup(f => f.GetClient<TestClient>()).Returns(_s3.Object);
        _factory.Setup(f => f.GetClientBucketName<TestClient>()).Returns(BucketName);
    }

    // -----------------------------------------------------------------------
    // WriteAsync
    // -----------------------------------------------------------------------

    [Fact]
    public async Task WriteAsync_ShouldCallPutObject_WithCorrectBucketAndKey()
    {
        _s3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new PutObjectResponse());

        var sut = new StorageService<TestClient>(_factory.Object);

        await sut.WriteAsync(SourceKey, Payload, TestContext.Current.CancellationToken);

        _s3.Verify(s => s.PutObjectAsync(
            It.Is<PutObjectRequest>(r =>
                r.BucketName == BucketName &&
                r.Key == SourceKey &&
                r.ContentBody == Payload),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task WriteAsync_ShouldPropagate_WhenS3Throws()
    {
        _s3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
           .ThrowsAsync(new AmazonS3Exception("S3 unavailable"));

        var sut = new StorageService<TestClient>(_factory.Object);

        await Assert.ThrowsAsync<AmazonS3Exception>(() =>
            sut.WriteAsync(SourceKey, Payload, TestContext.Current.CancellationToken));
    }

    // -----------------------------------------------------------------------
    // CopyAsync (move: copy then delete)
    // -----------------------------------------------------------------------

    [Fact]
    public async Task CopyAsync_ShouldCopyObjectToTargetKey()
    {
        _s3.Setup(s => s.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new CopyObjectResponse());
        _s3.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new DeleteObjectResponse());

        var sut = new StorageService<TestClient>(_factory.Object);

        await sut.CopyAsync(SourceKey, TargetKey, TestContext.Current.CancellationToken);

        _s3.Verify(s => s.CopyObjectAsync(
            It.Is<CopyObjectRequest>(r =>
                r.SourceBucket == BucketName &&
                r.SourceKey == SourceKey &&
                r.DestinationBucket == BucketName &&
                r.DestinationKey == TargetKey),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CopyAsync_ShouldDeleteSourceObject_AfterSuccessfulCopy()
    {
        _s3.Setup(s => s.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new CopyObjectResponse());
        _s3.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new DeleteObjectResponse());

        var sut = new StorageService<TestClient>(_factory.Object);

        await sut.CopyAsync(SourceKey, TargetKey, TestContext.Current.CancellationToken);

        _s3.Verify(s => s.DeleteObjectAsync(
            It.Is<DeleteObjectRequest>(r =>
                r.BucketName == BucketName &&
                r.Key == SourceKey),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CopyAsync_ShouldNotDeleteSource_WhenCopyFails()
    {
        _s3.Setup(s => s.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), It.IsAny<CancellationToken>()))
           .ThrowsAsync(new AmazonS3Exception("Copy failed"));

        var sut = new StorageService<TestClient>(_factory.Object);

        await Assert.ThrowsAsync<AmazonS3Exception>(() =>
            sut.CopyAsync(SourceKey, TargetKey, TestContext.Current.CancellationToken));

        _s3.Verify(s => s.DeleteObjectAsync(
            It.IsAny<DeleteObjectRequest>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CopyAsync_ShouldPropagate_WhenDeleteFails()
    {
        _s3.Setup(s => s.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new CopyObjectResponse());
        _s3.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
           .ThrowsAsync(new AmazonS3Exception("Delete failed"));

        var sut = new StorageService<TestClient>(_factory.Object);

        await Assert.ThrowsAsync<AmazonS3Exception>(() =>
            sut.CopyAsync(SourceKey, TargetKey, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CopyAsync_ShouldPerformCopyBeforeDelete()
    {
        var callOrder = new List<string>();

        _s3.Setup(s => s.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), It.IsAny<CancellationToken>()))
           .Callback<CopyObjectRequest, CancellationToken>((_, _) => callOrder.Add("Copy"))
           .ReturnsAsync(new CopyObjectResponse());
        _s3.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
           .Callback<DeleteObjectRequest, CancellationToken>((_, _) => callOrder.Add("Delete"))
           .ReturnsAsync(new DeleteObjectResponse());

        var sut = new StorageService<TestClient>(_factory.Object);

        await sut.CopyAsync(SourceKey, TargetKey, TestContext.Current.CancellationToken);

        callOrder.Should().ContainInOrder("Copy", "Delete");
    }
}