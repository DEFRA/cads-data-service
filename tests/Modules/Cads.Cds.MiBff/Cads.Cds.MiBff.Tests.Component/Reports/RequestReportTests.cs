using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class RequestReportTests
{
    private static string Endpoint =>
        $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleDeathsReportKey}/jobs";

    [Fact]
    public async Task GivenValidRequest_WhenRequestReportRequested_ShouldReturnAcceptedWithJobId()
    {
        var request = new GetGbCattleDeathsRequest { Year = 2026 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var response = await client.PostAsync(Endpoint, payload, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var body = await response.Content.ReadFromJsonAsync<CreateReportJobResultDto>(TestContext.Current.CancellationToken);
        body.Should().NotBeNull();
        body!.JobId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenUnknownReportKey_WhenRequestReportRequested_ShouldReturnForbidden()
    {
        var request = new GetGbCattleDeathsRequest { Year = 2026 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var response = await client.PostAsync(
            "/api/v1/bff/mi/reports/unknown_report_key/jobs",
            payload,
            TestContext.Current.CancellationToken);

        // The user has no granted permission for an unknown reportKey, so the
        // dynamic ReportAccess policy denies the request before the action runs.
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenNullJson_WhenRequestReportRequested_ShouldReturnBadRequest()
    {
        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var response = await client.PostAsync(
            Endpoint,
            new StringContent("null", System.Text.Encoding.UTF8, "application/json"),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenJobCreated_WhenGetReportJobStatusRequested_ShouldReturnPendingStatus()
    {
        var request = new GetGbCattleDeathsRequest { Year = 2026 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var createResponse = await client.PostAsync(Endpoint, payload, TestContext.Current.CancellationToken);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var created = await createResponse.Content.ReadFromJsonAsync<CreateReportJobResultDto>(TestContext.Current.CancellationToken);
        created.Should().NotBeNull();

        var statusEndpoint = $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleDeathsReportKey}/jobs/{created!.JobId}";

        var statusResponse = await client.GetAsync(statusEndpoint, TestContext.Current.CancellationToken);
        statusResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var status = await statusResponse.Content.ReadFromJsonAsync<ReportJobStatusDto>(TestContext.Current.CancellationToken);
        status.Should().NotBeNull();
        status!.JobId.Should().Be(created.JobId);
        status.Status.Should().Be("pending");
        status.IsReady.Should().BeFalse();
    }

    [Fact]
    public async Task GivenUnknownJobId_WhenGetReportJobStatusRequested_ShouldReturnNotFound()
    {
        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var statusEndpoint = $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleDeathsReportKey}/jobs/{Guid.NewGuid()}";

        var response = await client.GetAsync(statusEndpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}