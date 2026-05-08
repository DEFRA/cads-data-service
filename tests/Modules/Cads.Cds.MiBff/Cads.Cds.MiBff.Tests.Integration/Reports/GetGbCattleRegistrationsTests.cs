using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Testing.Support.Constants;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetGbCattleRegistrationsTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenInvalidYear_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest("{\"year\":0,\"month\":4}");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenInvalidMonth_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest("{\"year\":2026,\"month\":0}");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        var response = await ExecuteTest("{\"year\":2026,\"month\":4}");

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private async Task<HttpResponseMessage> ExecuteTest(string request)
    {
        var endpoint = TestEndpointConstants.BffMiReportsGetGbCattleRegistrationsEndpoint;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        return await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
    }
}