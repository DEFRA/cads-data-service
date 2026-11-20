using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.CircuitBreakers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Endpoints.Promotion.Responses;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Testing.Support.Utilities.Postgres;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class PromoteEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    private readonly PostgresDb _postgresDb = new(apiContainerFixture.PostgresFixture.HostConnectionString);

    [Fact]
    public async Task GivenPromotionTypeIsDisabled_WhenPromoteImportRequested_ShouldReturnBadRequest()
    {
        await ResetUpdateAllCtsParallelImportRunStatusAsync();

        var correlationId = Guid.NewGuid().ToString();

        var response = await ExecuteAsync(
            PromotionType.CtsDelta,
            correlationId);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenPromotionTypeIsEnabled_WhenPromoteImportRequested_ShouldReturnAcceptedAndQueueJob()
    {
        await ResetUpdateAllCtsParallelImportRunStatusAsync();

        var correlationId = Guid.NewGuid().ToString();

        var promoteImportResponse = await ExecuteWhenQueueFreeAsync(
            PromotionType.CtsBulk,
            correlationId);

        promoteImportResponse.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var promoteImportDto = await PromoteTestClient.ReadDtoAsync<PromoteImportResponse>(
            promoteImportResponse,
            TestContext.Current.CancellationToken);

        promoteImportDto.Should().NotBeNull();
        promoteImportDto.CorrelationId.Should().Be(correlationId);
        promoteImportDto.Status.Should().Be("Accepted");

        var ctsImportDto = await CircuitBreakerUtilities.WaitUntilAsync(
            async () =>
            {
                using var ctsImportResponse = await DbAdminTestClient.ExecuteCtsImportAsync(
                    _httpClient,
                    command: "runs",
                    args: new { correlation_id = correlationId },
                    TestContext.Current.CancellationToken);

                return await DbAdminTestClient.ReadDtoAsync(
                    ctsImportResponse,
                    TestContext.Current.CancellationToken);
            },
            dto => dto is { Result.ValueKind: JsonValueKind.Array } && dto.Result.GetArrayLength() > 0,
            timeout: TimeSpan.FromSeconds(30),
            pollInterval: TimeSpan.FromSeconds(1));

        var firstRow = ctsImportDto!.Result
            .EnumerateArray()
            .Single();

        firstRow.GetProperty("correlation_id")
            .GetString()
            .Should()
            .Be(correlationId);

        await _postgresDb.UpdateCtsParallelImportRunStatusByCorrelationIdAsync(correlationId, PromotionPostgresDbCommandHelper.ImportRunStatusComplete);
    }

    private async Task<HttpResponseMessage> ExecuteAsync(PromotionType promotionType, string correlationId)
    {
        return await PromoteTestClient.PromoteImportAsync(
            _httpClient,
            promotionType,
            correlationId,
            TestContext.Current.CancellationToken);
    }

    private async Task<HttpResponseMessage> ExecuteWhenQueueFreeAsync(PromotionType promotionType, string correlationId)
    {
        var deadline = DateTime.UtcNow.AddSeconds(60);

        while (true)
        {
            var response = await ExecuteAsync(promotionType, correlationId);

            if (response.StatusCode != HttpStatusCode.Conflict || DateTime.UtcNow >= deadline)
            {
                return response;
            }

            response.Dispose();
            await Task.Delay(TimeSpan.FromMilliseconds(500), TestContext.Current.CancellationToken);
        }
    }

    private async Task ResetUpdateAllCtsParallelImportRunStatusAsync()
    {
        await _postgresDb.UpdateAllCtsParallelImportRunStatusAsync(PromotionPostgresDbCommandHelper.ImportRunStatusComplete);
    }
}