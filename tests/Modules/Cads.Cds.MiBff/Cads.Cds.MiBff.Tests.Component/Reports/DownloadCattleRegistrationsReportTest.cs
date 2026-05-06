using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class DownloadCattleRegistrationsReportTest
{
    [Fact]
    public async Task GivenValidUser_WhenDownloadCattleRegistrationsReportRequested_ShouldReturnReport()
    {
        var mockRepo = new Mock<IMiReportRepository>();
        var resultRows = new List<MiBirthSummaryResult>();

        var factory = 
            new MiBffWebApplicationFactory(
                useFakeAuth: true,
                serviceOverrides: new List<Action<IServiceCollection>>
                    { (x) =>
                    {
                        x.RemoveAll<IMiReportRepository>();
                        x.AddScoped<IMiReportRepository>(_ => mockRepo.Object);
                    } });

        var client = factory.CreateClient();
        client.AddJwt();
        
        mockRepo.Setup(x => x.GetBirthSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultRows);

        var endpoint = "/api/v1/bff/mi/reports/gb_cattle_registrations";
        var request = new GetMonthlyCattleRegistrationReportRequest { Year = 2026, Month = 4 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}