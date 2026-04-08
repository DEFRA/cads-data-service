using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class ReportsEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetUserReportsRequested_ShouldReturnReports()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(new TestTokenRequest
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes = ["openid", "profile", "email", TestAuthConstants.AzureAdCadsCdsScope]
        });

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseBody.Should().NotBeNull().And.Contain("reportId");
    }

    [Fact]
    public async Task GivenScopeMissing_WhenGetUserReportsRequested_ShouldFailWithUnauthorized()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(new TestTokenRequest
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes = ["openid", "profile", "email"]
        });

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenInvalidScope_WhenGetUserReportsRequested_ShouldFailWithForbidden()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(new TestTokenRequest
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes = ["openid", "profile", "email", "reports.none"]
        });

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenUserHasNoReportAccess_WhenGetUserReportsRequested_ShouldReturnEmpty()
    {
        var endpoint = TestEndpointConstants.BffMiReportsRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(new TestTokenRequest
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = "unknown-user",
            Password = TestAuthConstants.AzureAdPassword,
            Scopes = ["openid", "profile", "email", TestAuthConstants.AzureAdCadsCdsScope]
        });

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        body.Should().Be("[]");
    }
}