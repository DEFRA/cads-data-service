using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Readers;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Storage.Readers;

public class BulkImportStorageReaderTests
{
    private class TestClient : IStorageClient
    {
        public string ClientName => GetType().Name;
    }

    [Fact]
    public async Task GetObjectResponseAsync_ShouldReturnS3Response()
    {
        // Arrange
        var s3 = new Mock<IAmazonS3>();
        var factory = new Mock<IS3ClientFactory>();
        var response = new GetObjectResponse();

        factory.Setup(f => f.GetClient<TestClient>())
               .Returns(s3.Object);

        factory.Setup(f => f.GetClientBucketName<TestClient>())
               .Returns("test-bucket");

        s3.Setup(s => s.GetObjectAsync(
                It.Is<GetObjectRequest>(r => r.BucketName == "test-bucket" && r.Key == "LOCATIONS.part-0001.csv"),
                It.IsAny<CancellationToken>()))
          .ReturnsAsync(response);

        var reader = new BulkImportStorageReader<TestClient>(factory.Object);

        // Act
        var result = await reader.GetObjectResponseAsync("LOCATIONS.part-0001.csv", TestContext.Current.CancellationToken);

        // Assert
        result.Should().Be(response);
    }

    [Fact]
    public async Task ListKeysAsync_ShouldReturnPrefix_WhenPrefixIsFile()
    {
        // Arrange
        var s3 = new Mock<IAmazonS3>();
        var factory = new Mock<IS3ClientFactory>();

        factory.Setup(f => f.GetClient<TestClient>())
               .Returns(s3.Object);

        factory.Setup(f => f.GetClientBucketName<TestClient>())
               .Returns("bucket");

        s3.Setup(s => s.GetObjectMetadataAsync("bucket", "path/to/LOCATIONS.part-0001.csv", It.IsAny<CancellationToken>()))
          .ReturnsAsync(new GetObjectMetadataResponse());

        var reader = new BulkImportStorageReader<TestClient>(factory.Object);

        // Act
        var result = await reader.ListKeysAsync("path/to/LOCATIONS.part-0001.csv", TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeEquivalentTo(["path/to/LOCATIONS.part-0001.csv"]);
    }

    [Fact]
    public async Task ListKeysAsync_ShouldListKeys_WhenPrefixIsFolder()
    {
        // Arrange
        var s3 = new Mock<IAmazonS3>();
        var factory = new Mock<IS3ClientFactory>();

        factory.Setup(f => f.GetClient<TestClient>())
               .Returns(s3.Object);

        factory.Setup(f => f.GetClientBucketName<TestClient>())
               .Returns("bucket");

        s3.Setup(s => s.GetObjectMetadataAsync("bucket", "folder", It.IsAny<CancellationToken>()))
          .ThrowsAsync(new AmazonS3Exception("not found", ErrorType.Sender, "", "", HttpStatusCode.NotFound));

        s3.Setup(s => s.ListObjectsV2Async(
                It.Is<ListObjectsV2Request>(r => r.Prefix == "folder/"),
                It.IsAny<CancellationToken>()))
          .ReturnsAsync(new ListObjectsV2Response
          {
              S3Objects =
              [
                  new() { Key = "folder/a.csv" },
                  new() { Key = "folder/b.csv" }
              ],
              IsTruncated = false
          });

        var reader = new BulkImportStorageReader<TestClient>(factory.Object);

        // Act
        var result = await reader.ListKeysAsync("folder", TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeEquivalentTo(
        [
            "folder/a.csv",
            "folder/b.csv"
        ]);
    }

    [Fact]
    public async Task ListKeysAsync_ShouldReturnEmpty_WhenPrefixNotFound()
    {
        // Arrange
        var s3 = new Mock<IAmazonS3>();
        var factory = new Mock<IS3ClientFactory>();

        factory.Setup(f => f.GetClient<TestClient>())
               .Returns(s3.Object);

        factory.Setup(f => f.GetClientBucketName<TestClient>())
               .Returns("bucket");

        // Not a file
        s3.Setup(s => s.GetObjectMetadataAsync("bucket", "missing", It.IsAny<CancellationToken>()))
          .ThrowsAsync(new AmazonS3Exception("not found", ErrorType.Sender, "", "", HttpStatusCode.NotFound));

        // Not a folder
        s3.Setup(s => s.ListObjectsV2Async(
                It.IsAny<ListObjectsV2Request>(),
                It.IsAny<CancellationToken>()))
          .ReturnsAsync(new ListObjectsV2Response
          {
              S3Objects = [],
              IsTruncated = false
          });

        var reader = new BulkImportStorageReader<TestClient>(factory.Object);

        // Act
        var result = await reader.ListKeysAsync("missing", TestContext.Current.CancellationToken);

        // Assert
        result.Should().BeEmpty();
    }
}