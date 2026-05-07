using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetHoldingSummaryTests
{
    [Fact]
    public async Task GivenValidRequest_WhenGetHoldingSummaryRequested_ShouldSucceed()
    {
        var response = await ExecuteTest();

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private static MiBffWebApplicationFactory GetFactory(bool useFakeAuth = true)
    {
        var factory = new MiBffWebApplicationFactory(useFakeAuth: useFakeAuth);

        return factory;
    }

    private async Task<HttpResponseMessage?> ExecuteTest()
    {
        var endpoint = $"/api/v1/bff/mi/reports/{TestReportKeyConstants.HoldingSummaryReportKey}";
        var request = new GetHoldingSummaryRequest();
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var factory = GetFactory();
        var client = factory.CreateClient();
        client.AddJwt();

        return await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
    }
}