using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.StorageBridge.Tests.Component.StorageManagement;

public class StorageManagementAuthorizationEndpointTests : IClassFixture<StorageManagementTestFixture>
{
    private const string Endpoint = TestEndpointConstants.StorageBridgeStorageManagementRoot;

    private readonly StorageManagementTestFixture _testFixture;

    public StorageManagementAuthorizationEndpointTests(StorageManagementTestFixture testFixture)
    {
        _testFixture = testFixture;
        _testFixture.Factory.ResetMocks();
    }

    [Fact]
    public async Task GivenBasicToken_WhenBucketsListed_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);

        var response = await client.GetAsync($"{Endpoint}/buckets", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenTokenMissingScope_WhenBucketsListed_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingS3AdminScope);

        var response = await client.GetAsync($"{Endpoint}/buckets", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}