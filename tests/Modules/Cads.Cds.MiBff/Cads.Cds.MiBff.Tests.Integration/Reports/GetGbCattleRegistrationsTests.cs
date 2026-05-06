using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Testing.Support.Constants;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetGbCattleRegistrationsTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        var endpoint = TestEndpointConstants.BffMiReportsGetGbCattleRegistrationsEndpoint;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent("{\"year\":2026,\"month\":4}");

        var response = await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}