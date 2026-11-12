using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Providers;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Encryption;
using Cads.Cds.StorageBridge.Endpoints.Responses;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Testing.Support.Factories.Authorization;
using FluentAssertions;
using System.Net;
using System.Text;
using System.Net.Http.Json;

namespace Cads.Cds.StorageBridge.Tests.Integration.StorageManagement;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class StorageManagementEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private const string BucketsEndpoint = TestEndpointConstants.StorageBridgeStorageManagementRoot + "/buckets";

    [Fact]
    public async Task GivenValidRoleAndScope_WhenBucketsListed_ShouldReturnConfiguredBuckets()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestAdminS3ManagerTokenFactory.ValidUserToken());

        var response = await client.GetAsync(BucketsEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var buckets = await response.Content.ReadFromJsonAsync<List<StorageBucketResponse>>(
            TestContext.Current.CancellationToken);

        buckets.Should().BeEquivalentTo(
        [
            new StorageBucketResponse("CadsInternalClient", LocalStackFixture.CadsInternalBucketName),
            new StorageBucketResponse("CadsExternalClient", LocalStackFixture.CadsExternalBucketName)
        ]);
    }

    [Theory]
    [InlineData("CadsInternalClient", LocalStackFixture.CadsInternalBucketName)]
    public async Task GivenFileEncryptorEncryptedObject_WhenObjectFetched_ShouldReturnDecryptedContent(
        string clientName,
        string bucketName)
    {
        var key = $"storage-management/{Guid.NewGuid():N}/encrypted-object.txt";
        const string plaintext = "Sensitive data encrypted before being stored in S3.";
        var encryptor = new FileEncryptor(new InMemoryMasterKeyProvider(
            TestEncryptionKeyProvider.CurrentKeyId,
            TestEncryptionKeyProvider.Keys));
        using var encrypted = await EncryptionUtilities.EncryptAsync(encryptor, plaintext);
        encrypted.ToArray().Should().NotEqual(Encoding.UTF8.GetBytes(plaintext));

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = encrypted,
            ContentType = "text/plain"
        }, TestContext.Current.CancellationToken);

        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestAdminS3ManagerTokenFactory.ValidUserToken());
        var response = await client.GetAsync(
            $"{BucketsEndpoint}/{clientName}/object?key={Uri.EscapeDataString(key)}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");
        (await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken)).Should().Be(plaintext);
    }

    [Fact]
    public async Task GivenScopeMissing_WhenBucketsListed_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestAdminS3ManagerTokenFactory.MissingScopeToken());

        var response = await client.GetAsync(BucketsEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}