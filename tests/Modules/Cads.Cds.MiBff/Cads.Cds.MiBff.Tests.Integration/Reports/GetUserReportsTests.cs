using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.MiBff.Testing.Support.Constants;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetUserReportsTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetUserReportsRequested_ShouldReturnReports()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseBody.Should().NotBeNull().And.Contain("reportId");
    }


    [Fact]
    public async Task GivenScopeMissing_WhenGetUserReportsRequested_ShouldFailWithUnauthorized()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.MissingScopeToken());

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenInvalidScope_WhenGetUserReportsRequested_ShouldFailWithForbidden()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.InvalidScopeToken());

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenUserHasNoReportAccess_WhenGetUserReportsRequested_ShouldReturnEmpty()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ForUser("unknown-user"));

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        body.Should().Be("[]");
    }
}