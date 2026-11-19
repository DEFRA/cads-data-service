using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Endpoints.Promotion.Responses;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class PromoteEndpointTests(SystemAdminTestFixture testFixture) : IClassFixture<SystemAdminTestFixture>
{
    private readonly SystemAdminTestFixture _testFixture = testFixture;
    private HttpClient _httpClient => _testFixture.HttpClient;

    [Fact]
    public async Task GivenNoImportInProgress_WhenPromoteImportRequested_ShouldReturnAcceptedAndQueueJob()
    {
        _testFixture.Factory.PromotionJobQueueMock.Reset();
        _testFixture.Factory.PromotionJobQueueMock
            .Setup(x => x.TryEnqueue(It.IsAny<PromotionType>(), It.IsAny<string>(), It.IsAny<bool?>(), It.IsAny<string?>()))
            .Returns(true);

        var correlationId = Guid.NewGuid().ToString();

        var response = await PromoteTestClient.PromoteImportAsync(
            _httpClient,
            PromotionType.CtsBulk,
            correlationId,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        _testFixture.Factory.PromotionJobQueueMock.Verify(
            x => x.TryEnqueue(It.IsAny<PromotionType>(), It.IsAny<string>(), It.IsAny<bool?>(), It.IsAny<string?>()), Times.Once);

        var dto = await PromoteTestClient.ReadDtoAsync<PromoteImportResponse>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.CorrelationId.Should().Be(correlationId);
        dto.Status.Should().Be("Accepted");
    }

    [Fact]
    public async Task GivenImportAlreadyInProgress_WhenPromoteImportRequested_ShouldReturnConflict()
    {
        _testFixture.Factory.PromotionJobQueueMock.Reset();
        _testFixture.Factory.PromotionJobQueueMock
            .Setup(x => x.TryEnqueue(It.IsAny<PromotionType>(), It.IsAny<string>(), It.IsAny<bool?>(), It.IsAny<string?>()))
            .Returns(false);

        var correlationId = Guid.NewGuid().ToString();

        var response = await PromoteTestClient.PromoteImportAsync(
            _httpClient,
            PromotionType.CtsBulk,
            correlationId,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenPromotionTypeIsDisabled_WhenPromoteImportRequested_ShouldReturnBadRequest()
    {
        _testFixture.Factory.PromotionJobQueueMock.Reset();
        _testFixture.Factory.PromotionJobQueueMock
            .Setup(x => x.TryEnqueue(It.IsAny<PromotionType>(), It.IsAny<string>(), It.IsAny<bool?>(), It.IsAny<string?>()))
            .Returns(false);

        var correlationId = Guid.NewGuid().ToString();

        var response = await PromoteTestClient.PromoteImportAsync(
            _httpClient,
            PromotionType.CtsDelta,
            correlationId,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenPromotionEndpointsDisabled_WhenPromoteImportRequested_ShouldReturnNotFound()
    {
        await using var factory = new SystemAdminWebApplicationFactory(
            configOverrides: new Dictionary<string, string?>
            {
                ["Modules:SystemAdmin:EnablePromotionEndpoints"] = "false"
            },
            useFakeAuth: true);

        var client = factory.CreateClient();
        client.AddJwt();

        var correlationId = Guid.NewGuid().ToString();

        var response = await PromoteTestClient.PromoteImportAsync(
            client,
            PromotionType.CtsBulk,
            correlationId,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenScopeClaimMissing_WhenPromoteRequested_ShouldReturnForbidden()
    {
        var client = _testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingDbAdminScope);

        var correlationId = Guid.NewGuid().ToString();

        var response = await PromoteTestClient.PromoteImportAsync(
            client,
            PromotionType.CtsBulk,
            correlationId,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }    
}
