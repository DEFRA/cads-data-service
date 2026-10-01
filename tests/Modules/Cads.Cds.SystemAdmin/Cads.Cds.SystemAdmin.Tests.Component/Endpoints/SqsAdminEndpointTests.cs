using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class SqsAdminEndpointTests(SystemAdminTestFixture testFixture) : IClassFixture<SystemAdminTestFixture>
{
    private HttpClient _httpClient => testFixture.HttpClient;

    // GetQueues

    [Fact]
    public async Task GivenQueuesConfigured_WhenGetQueuesRequested_ShouldReturnOk()
    {
        var queues = new List<QueueInfoDto>
        {
            new("CadsCds", "https://sqs.test/queue", "https://sqs.test/queue-dlq")
        };

        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.GetQueuesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(queues);

        var response = await SqsAdminTestClient.GetQueuesAsync(_httpClient, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await SqsAdminTestClient.ReadQueuesDtoAsync(response, TestContext.Current.CancellationToken);
        dto.Should().NotBeNull();
        dto!.Queues.Should().ContainSingle(q => q.Name == "CadsCds");
    }

    [Fact]
    public async Task GivenNoToken_WhenGetQueuesRequested_ShouldReturnUnauthorized()
    {
        var client = testFixture.Factory.CreateClient();

        var response = await SqsAdminTestClient.GetQueuesAsync(client, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenRoleClaimMissing_WhenGetQueuesRequested_ShouldReturnForbidden()
    {
        var client = testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingDbAdminRole);

        var response = await SqsAdminTestClient.GetQueuesAsync(client, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenScopeClaimMissing_WhenGetQueuesRequested_ShouldReturnForbidden()
    {
        var client = testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingDbAdminScope);

        var response = await SqsAdminTestClient.GetQueuesAsync(client, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenSqsAdminEndpointsDisabled_WhenGetQueuesRequested_ShouldReturnNotFound()
    {
        await using var factory = new SystemAdminWebApplicationFactory(
            configOverrides: new Dictionary<string, string?>
            {
                ["Modules:SystemAdmin:EnableAdminEndpoints"] = "false"
            },
            useFakeAuth: true);

        var client = factory.CreateClient();
        client.AddJwt();

        var response = await SqsAdminTestClient.GetQueuesAsync(client, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // GetMetrics

    [Fact]
    public async Task GivenValidQueue_WhenGetMetricsRequested_ShouldReturnOk()
    {
        var metrics = new QueueMetricsDto(3, 1, 0, 42);

        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.GetMetricsAsync("CadsCds", It.IsAny<CancellationToken>()))
            .ReturnsAsync(metrics);

        var response = await SqsAdminTestClient.GetMetricsAsync(_httpClient, "CadsCds", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await SqsAdminTestClient.ReadMetricsDtoAsync(response, TestContext.Current.CancellationToken);
        dto.Should().NotBeNull();
        dto!.Queue.Should().Be("CadsCds");
        dto.Metrics.ApproximateNumberOfMessages.Should().Be(3);
    }

    [Fact]
    public async Task GivenUnknownQueue_WhenGetMetricsRequested_ShouldReturnNotFound()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.GetMetricsAsync("unknown-queue", It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Cads.Cds.BuildingBlocks.Core.Exceptions.NotFoundException("Queue", "unknown-queue"));

        var response = await SqsAdminTestClient.GetMetricsAsync(_httpClient, "unknown-queue", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenNoToken_WhenGetMetricsRequested_ShouldReturnUnauthorized()
    {
        var client = testFixture.Factory.CreateClient();

        var response = await SqsAdminTestClient.GetMetricsAsync(client, "CadsCds", TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // GetMessages

    [Fact]
    public async Task GivenValidQueue_WhenGetMessagesRequested_ShouldReturnOk()
    {
        var messages = new List<QueueMessageDto>
        {
            new("msg-1", "{\"foo\":1}", new Dictionary<string, string>(), DateTimeOffset.UtcNow)
        };

        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.PeekMessagesAsync(
                It.Is<PeekMessagesRequestDto>(r => r.Queue == "CadsCds" && r.MaxMessages == 10),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(messages);

        var response = await SqsAdminTestClient.GetMessagesAsync(_httpClient, "CadsCds", maxMessages: null, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await SqsAdminTestClient.ReadMessagesDtoAsync(response, TestContext.Current.CancellationToken);
        dto.Should().NotBeNull();
        dto!.Messages.Should().ContainSingle(m => m.MessageId == "msg-1");
    }

    [Fact]
    public async Task GivenMaxMessagesSupplied_WhenGetMessagesRequested_ShouldPassThroughToService()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.PeekMessagesAsync(
                It.Is<PeekMessagesRequestDto>(r => r.Queue == "CadsCds" && r.MaxMessages == 3),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var response = await SqsAdminTestClient.GetMessagesAsync(_httpClient, "CadsCds", maxMessages: 3, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        testFixture.Factory.SqsAdminServiceMock.Verify(x => x.PeekMessagesAsync(
            It.Is<PeekMessagesRequestDto>(r => r.MaxMessages == 3),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GivenUnknownQueue_WhenGetMessagesRequested_ShouldReturnNotFound()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.PeekMessagesAsync(
                It.Is<PeekMessagesRequestDto>(r => r.Queue == "unknown-queue"),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Cads.Cds.BuildingBlocks.Core.Exceptions.NotFoundException("Queue", "unknown-queue"));

        var response = await SqsAdminTestClient.GetMessagesAsync(_httpClient, "unknown-queue", maxMessages: null, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ReplayDlq

    [Fact]
    public async Task GivenValidRequest_WhenReplayRequested_ShouldReturnOkWithResult()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.ReplayDlqAsync(
                It.Is<Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin.ReplayDlqRequestDto>(r => r.Queue == "CadsCds" && r.BatchSize == 5),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReplayResultDto(4, 1, ["MessageId 'm1': boom"]));

        var response = await SqsAdminTestClient.ReplayDlqAsync(_httpClient, "CadsCds", batchSize: 5, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await SqsAdminTestClient.ReadReplayDtoAsync(response, TestContext.Current.CancellationToken);
        dto.Should().NotBeNull();
        dto!.Queue.Should().Be("CadsCds");
        dto.Moved.Should().Be(4);
        dto.Failed.Should().Be(1);
        dto.Errors.Should().ContainSingle();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public async Task GivenInvalidBatchSize_WhenReplayRequested_ShouldReturnBadRequest(int batchSize)
    {
        var response = await SqsAdminTestClient.ReplayDlqAsync(_httpClient, "CadsCds", batchSize, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await SqsAdminTestClient.ReadProblemDetailsAsync(response, TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails!.Extensions.Should().ContainKey("errors");
    }

    [Fact]
    public async Task GivenNoBatchSize_WhenReplayRequested_ShouldSucceed()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.ReplayDlqAsync(
                It.Is<Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin.ReplayDlqRequestDto>(r => r.Queue == "CadsCds" && r.BatchSize == 0),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReplayResultDto(0, 0, []));

        var response = await SqsAdminTestClient.ReplayDlqAsync(_httpClient, "CadsCds", batchSize: null, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GivenQueueWithoutDlq_WhenReplayRequested_ShouldReturnUnprocessable()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.ReplayDlqAsync(
                It.Is<Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin.ReplayDlqRequestDto>(r => r.Queue == "CadsCds"),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Cads.Cds.BuildingBlocks.Core.Exceptions.UnprocessableException("Queue 'CadsCds' does not have a configured DLQ."));

        var response = await SqsAdminTestClient.ReplayDlqAsync(_httpClient, "CadsCds", batchSize: 5, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task GivenNoToken_WhenReplayRequested_ShouldReturnUnauthorized()
    {
        var client = testFixture.Factory.CreateClient();

        var response = await SqsAdminTestClient.ReplayDlqAsync(client, "CadsCds", batchSize: 5, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GivenRoleClaimMissing_WhenReplayRequested_ShouldReturnForbidden()
    {
        var client = testFixture.Factory.CreateClient();
        client.AddJwt(TestAuthConstants.FakeJwtMissingDbAdminRole);

        var response = await SqsAdminTestClient.ReplayDlqAsync(client, "CadsCds", batchSize: 5, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GivenValidRequest_WhenReplayRequested_ShouldLogAndInvokeServiceOnce()
    {
        testFixture.Factory.SqsAdminServiceMock
            .Setup(x => x.ReplayDlqAsync(
                It.Is<Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin.ReplayDlqRequestDto>(r => r.Queue == "CadsCds" && r.BatchSize == 7),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReplayResultDto(7, 0, []));

        var response = await SqsAdminTestClient.ReplayDlqAsync(_httpClient, "CadsCds", batchSize: 7, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        testFixture.Factory.SqsAdminServiceMock.Verify(x => x.ReplayDlqAsync(
            It.Is<Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin.ReplayDlqRequestDto>(r => r.Queue == "CadsCds" && r.BatchSize == 7),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}