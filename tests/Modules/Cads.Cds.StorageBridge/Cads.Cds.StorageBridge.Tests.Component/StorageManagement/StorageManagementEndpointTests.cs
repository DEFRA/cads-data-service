using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;
using System.Text;

namespace Cads.Cds.StorageBridge.Tests.Component.StorageManagement;

public class StorageManagementEndpointTests : IClassFixture<StorageManagementTestFixture>
{
    private const string Endpoint = TestEndpointConstants.StorageBridgeStorageManagementRoot;
    private const string ObjectKey = "folder/upload-test.txt";
    private const string ObjectBody = "hello from the management api";

    private readonly StorageManagementTestFixture _testFixture;

    public StorageManagementEndpointTests(StorageManagementTestFixture testFixture)
    {
        _testFixture = testFixture;
        _testFixture.Factory.ResetMocks();
    }

    [Fact]
    public async Task GivenInternalClient_WhenObjectUploaded_ShouldPutToInternalBucket()
    {
        var (captured, body) = SetupPutObjectCapture();

        var response = await _testFixture.HttpClient.PutAsync(
            $"{Endpoint}/buckets/CadsInternalClient/object?key={Uri.EscapeDataString(ObjectKey)}",
            new StringContent(ObjectBody, Encoding.UTF8, "text/plain"),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        captured().Should().NotBeNull();
        captured()!.BucketName.Should().Be(TestS3Constants.TestCadsInternalBucketName);
        captured()!.Key.Should().Be(ObjectKey);
        captured()!.ContentType.Should().StartWith("text/plain");
        body().Should().Be(ObjectBody);
    }

    [Fact]
    public async Task GivenExternalClient_WhenObjectUploaded_ShouldPutToExternalBucket()
    {
        var (captured, body) = SetupPutObjectCapture();

        var response = await _testFixture.HttpClient.PutAsync(
            $"{Endpoint}/buckets/CadsExternalClient/object?key={Uri.EscapeDataString(ObjectKey)}",
            new StringContent(ObjectBody, Encoding.UTF8, "text/plain"),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        captured().Should().NotBeNull();
        captured()!.BucketName.Should().Be(TestS3Constants.TestCadsExternalBucketName);
        captured()!.Key.Should().Be(ObjectKey);
        body().Should().Be(ObjectBody);
    }

    [Fact]
    public async Task GivenUnknownClient_WhenObjectUploaded_ShouldReturnNotFound()
    {
        var response = await _testFixture.HttpClient.PutAsync(
            $"{Endpoint}/buckets/NoSuchClient/object?key={Uri.EscapeDataString(ObjectKey)}",
            new StringContent(ObjectBody, Encoding.UTF8, "text/plain"),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.PutObjectAsync(
            It.IsAny<PutObjectRequest>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenInternalClient_WhenObjectDeleted_ShouldDeleteFromInternalBucket()
    {
        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteObjectResponse());

        var response = await _testFixture.HttpClient.DeleteAsync(
            $"{Endpoint}/buckets/CadsInternalClient/object?key={Uri.EscapeDataString(ObjectKey)}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.DeleteObjectAsync(
            It.Is<DeleteObjectRequest>(r =>
                r.BucketName == TestS3Constants.TestCadsInternalBucketName &&
                r.Key == ObjectKey),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenExternalClient_WhenObjectDeleted_ShouldDeleteFromExternalBucket()
    {
        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteObjectResponse());

        var response = await _testFixture.HttpClient.DeleteAsync(
            $"{Endpoint}/buckets/CadsExternalClient/object?key={Uri.EscapeDataString(ObjectKey)}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.DeleteObjectAsync(
            It.Is<DeleteObjectRequest>(r =>
                r.BucketName == TestS3Constants.TestCadsExternalBucketName &&
                r.Key == ObjectKey),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenUnknownClient_WhenObjectDeleted_ShouldReturnNotFound()
    {
        var response = await _testFixture.HttpClient.DeleteAsync(
            $"{Endpoint}/buckets/NoSuchClient/object?key={Uri.EscapeDataString(ObjectKey)}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.DeleteObjectAsync(
            It.IsAny<DeleteObjectRequest>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    private (Func<PutObjectRequest?> Captured, Func<string?> Body) SetupPutObjectCapture()
    {
        PutObjectRequest? captured = null;
        string? body = null;

        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .Callback<PutObjectRequest, CancellationToken>((request, _) =>
            {
                captured = request;
                using var reader = new StreamReader(request.InputStream, leaveOpen: true);
                body = reader.ReadToEnd();
            })
            .ReturnsAsync(new PutObjectResponse());

        return (() => captured, () => body);
    }
}