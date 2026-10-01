using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.StorageBridge.Endpoints.Responses;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.StorageBridge.Tests.Integration.StorageManagement;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class StorageManagementEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private const string BucketsEndpoint = TestEndpointConstants.StorageBridgeStorageManagementRoot + "/buckets";

    [Fact]
    public async Task GivenValidRoleAndScope_WhenBucketsListed_ShouldReturnConfiguredBuckets()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.S3AdminToken());

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

    [Fact]
    public async Task GivenRoleMissing_WhenBucketsListed_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.S3AdminMissingRoleToken());

        var response = await client.GetAsync(BucketsEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenScopeMissing_WhenBucketsListed_ShouldReturnForbidden()
    {
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.S3AdminMissingScopeToken());

        var response = await client.GetAsync(BucketsEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenApiKeyCredentials_WhenBucketsListed_ShouldReturnUnauthorized()
    {
        var client = apiContainerFixture.CreateBasicClient();

        var response = await client.GetAsync(BucketsEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}