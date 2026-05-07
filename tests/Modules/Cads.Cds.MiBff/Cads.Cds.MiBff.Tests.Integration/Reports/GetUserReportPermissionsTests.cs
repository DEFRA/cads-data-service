using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.MiBff.Testing.Support.Constants;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetUserReportPermissionsTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetHoldingSummaryReportRequested_ShouldSucceed()
    {
        var endpoint = string.Format(TestEndpointConstants.BffMiReportsUserReportPermissionsEndpoint, TestReportKeyConstants.HoldingSummaryReportKey);
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}