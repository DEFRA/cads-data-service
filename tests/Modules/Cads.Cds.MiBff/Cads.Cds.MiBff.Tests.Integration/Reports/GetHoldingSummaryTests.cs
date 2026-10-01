using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Testing.Support.Factories.Authorization;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetHoldingSummaryTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetHoldingSummaryRequested_ShouldSucceed()
    {
        var endpoint = TestEndpointConstants.BffMiReportsGetHoldingSummaryEndpoint;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestReportsReadTokenFactory.ValidUserToken());

        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent("{}");

        var response = await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}