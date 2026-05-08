using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using MediatR;
using Moq;
using System.Net;
using System.Text;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetReportNegativeTests
{
    private static string Endpoint =>
        $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleRegistrationsReportKey}";

    private static Mock<IMiBirthSummaryRepository> CreateRepositoryMock() => new();

    private static Mock<IReportRegistry> CreateRegistryMock()
    {
        var mock = new Mock<IReportRegistry>();
        mock.Setup(r => r.Resolve(It.IsAny<string>(), It.IsAny<IServiceProvider>()))
            .Returns((new FakeHandler(), typeof(FakeRequest)));
        return mock;
    }

    private static HttpClient CreateClient(
        Mock<IReportRegistry> registryMock,
        Mock<IMiBirthSummaryRepository>? repoMock = null,
        Mock<IMediator>? mediatorMock = null)
    {
        var factory = new MiBffWebApplicationFactory(useFakeAuth: true);

        factory.OverrideServiceAsScoped(repoMock?.Object ?? new Mock<IMiBirthSummaryRepository>().Object);
        factory.OverrideServiceAsSingleton(registryMock.Object);

        if (mediatorMock is not null)
            factory.OverrideServiceAsSingleton(mediatorMock.Object);

        var client = factory.CreateClient();
        client.AddJwt();
        return client;
    }

    [Fact]
    public async Task GivenNullJson_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var client = CreateClient(
            CreateRegistryMock(),
            CreateRepositoryMock());

        var response = await client.PostAsync(
            Endpoint,
            new StringContent("null", Encoding.UTF8, "application/json"),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownReportResultType_WhenGetReportRequested_ShouldReturnBadRequest()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UnknownResultType());

        var client = CreateClient(
            CreateRegistryMock(),
            CreateRepositoryMock(),
            mediatorMock);

        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(
            new GetGbCattleRegistrationsRequest { Year = 2026, Month = 1 });

        var response = await client.PostAsync(
            Endpoint,
            payload,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private sealed class FakeRequest : GetReportRequest { }

    private sealed class FakeHandler : IReportHandler
    {
        public object BuildUntypedQuery(GetReportRequest request) => new();
    }

    private sealed class UnknownResultType { }
}