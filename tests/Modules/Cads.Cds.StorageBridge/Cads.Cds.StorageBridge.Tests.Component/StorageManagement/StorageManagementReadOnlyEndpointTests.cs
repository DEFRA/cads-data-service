using Amazon.S3.Model;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;
using System.Text;

namespace Cads.Cds.StorageBridge.Tests.Component.StorageManagement;

public class StorageManagementReadOnlyEndpointTests : IClassFixture<StorageManagementTestFixture>
{
    private const string Endpoint = TestEndpointConstants.StorageBridgeStorageManagementRoot;
    private const string ObjectKey = "folder/upload-test.txt";

    private readonly StorageManagementTestFixture _testFixture;

    public StorageManagementReadOnlyEndpointTests(StorageManagementTestFixture testFixture)
    {
        _testFixture = testFixture;
        _testFixture.Factory.ResetMocks();
    }

    [Theory]
    [InlineData("CadsInternalClient")]
    [InlineData("CadsExternalClient")]
    public async Task WhenObjectUploaded_ShouldReturnMethodNotAllowed(string clientName)
    {
        var response = await _testFixture.HttpClient.PutAsync(
            $"{Endpoint}/buckets/{clientName}/object?key={Uri.EscapeDataString(ObjectKey)}",
            new StringContent("content", Encoding.UTF8, "text/plain"),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.PutObjectAsync(
            It.IsAny<PutObjectRequest>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [InlineData("CadsInternalClient")]
    [InlineData("CadsExternalClient")]
    public async Task WhenObjectDeleted_ShouldReturnMethodNotAllowed(string clientName)
    {
        var response = await _testFixture.HttpClient.DeleteAsync(
            $"{Endpoint}/buckets/{clientName}/object?key={Uri.EscapeDataString(ObjectKey)}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.DeleteObjectAsync(
            It.IsAny<DeleteObjectRequest>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}