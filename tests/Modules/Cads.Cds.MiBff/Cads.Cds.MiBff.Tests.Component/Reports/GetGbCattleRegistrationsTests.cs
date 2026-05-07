using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetGbCattleRegistrationsTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidUser_WhenGetGbCattleRegistrationsRequested_ShouldReturnReport()
    {
        var mockRepo = new Mock<IMiBirthSummaryRepository>();
        var resultRows = new List<MiBirthSummary>();

        var factory =
            new MiBffWebApplicationFactory(
                useFakeAuth: true,
                serviceOverrides: new List<Action<IServiceCollection>>
                { (x) =>
                {
                    x.RemoveAll<IMiBirthSummaryRepository>();
                    x.AddScoped<IMiBirthSummaryRepository>(_ => mockRepo.Object);
                } });

        using var client = factory.CreateClient();
        client.AddJwt();

        mockRepo.Setup(x => x.GetBirthSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultRows);

        var endpoint = $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleRegistrationsReportKey}";
        var request = new GetGbCattleRegistrationsRequest { Year = 2026, Month = 4 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}