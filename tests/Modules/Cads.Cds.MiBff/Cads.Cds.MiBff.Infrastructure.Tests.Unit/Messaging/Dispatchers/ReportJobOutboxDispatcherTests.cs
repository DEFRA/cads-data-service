using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Reflection;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Messaging.Dispatchers;

public class ReportJobOutboxDispatcherTests
{
    private readonly Mock<IServiceScopeFactory> _scopeFactory = new();
    private readonly Mock<IServiceScope> _scope = new();
    private readonly Mock<IServiceProvider> _provider = new();
    private readonly Mock<IMiReportJobRepository> _repository = new();
    private readonly Mock<IMessagePublisher<MiBffFifoQueueClient>> _publisher = new();
    private readonly Mock<IOptionsMonitor<ReportJobOutboxDispatcherOptions>> _optionsMonitor = new();
    private readonly Mock<ILogger<ReportJobOutboxDispatcher>> _logger = new();

    private readonly ReportJobOutboxDispatcherOptions _options = new()
    {
        PollIntervalSeconds = 5,
        BatchSize = 20,
        MaxAttempts = 5
    };

    public ReportJobOutboxDispatcherTests()
    {
        _scopeFactory.Setup(x => x.CreateScope()).Returns(_scope.Object);
        _scope.Setup(x => x.ServiceProvider).Returns(_provider.Object);
        _provider.Setup(x => x.GetService(typeof(IMiReportJobRepository))).Returns(_repository.Object);
        _optionsMonitor.Setup(x => x.CurrentValue).Returns(_options);
    }

    private ReportJobOutboxDispatcher CreateSut()
        => new(_scopeFactory.Object, _publisher.Object, _optionsMonitor.Object, _logger.Object);

    private static MiReportJobOutbox CreateOutbox(Guid? reportJobId = null, string payload = "{}")
        => new()
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId ?? Guid.NewGuid(),
            Subject = "report.job.requested",
            Payload = payload,
            Status = ReportJobOutboxStatus.Dispatching,
            CreatedAt = DateTimeOffset.UtcNow
        };

    // -----------------------------------------------------------------------
    // DispatchPendingAsync (invoked directly via reflection - the private method
    // that ExecuteAsync's polling loop delegates to on each tick)
    // -----------------------------------------------------------------------

    [Fact]
    public async Task DispatchPendingAsync_WhenNoPendingMessages_DoesNotPublishOrDelete()
    {
        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var sut = CreateSut();

        await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

        _publisher.Verify(
            p => p.PublishAsync(It.IsAny<ReportJobRequestedMessage>(), It.IsAny<FifoMessageMetadata>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _repository.Verify(
            r => r.DeleteDispatchedOutboxMessagesAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenMessagesClaimed_PublishesEachAndDeletesAllDispatched()
    {
        var reportJobId = Guid.NewGuid();
        var outbox1 = CreateOutbox(reportJobId, $$"""{"reportJobId":"{{reportJobId}}","reportKey":"holdings_summary"}""");
        var outbox2 = CreateOutbox(reportJobId, $$"""{"reportJobId":"{{reportJobId}}","reportKey":"holdings_summary"}""");

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([outbox1, outbox2]);

        var sut = CreateSut();

        await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

        _publisher.Verify(
            p => p.PublishAsync(
                It.Is<ReportJobRequestedMessage>(m => m.Id == outbox1.OutboxId && m.JobId == reportJobId && m.ReportKey == "holdings_summary"),
                It.Is<FifoMessageMetadata>(m => m.MessageGroupId == reportJobId.ToString() && m.MessageDeduplicationId == outbox1.OutboxId.ToString()),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _publisher.Verify(
            p => p.PublishAsync(
                It.Is<ReportJobRequestedMessage>(m => m.Id == outbox2.OutboxId),
                It.IsAny<FifoMessageMetadata>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _repository.Verify(
            r => r.DeleteDispatchedOutboxMessagesAsync(
                It.Is<IEnumerable<Guid>>(ids => ids.ToHashSet().SetEquals(new[] { outbox1.OutboxId, outbox2.OutboxId })),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _repository.Verify(
            r => r.MarkOutboxDispatchFailedAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenPublishFailsForOneMessage_MarksOnlyThatOneFailedAndDeletesTheRest()
    {
        var succeeding = CreateOutbox();
        var failing = CreateOutbox();

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([succeeding, failing]);

        _publisher
            .Setup(p => p.PublishAsync(
                It.Is<ReportJobRequestedMessage>(m => m.Id == failing.OutboxId),
                It.IsAny<FifoMessageMetadata>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("SQS unavailable"));

        var sut = CreateSut();

        await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

        _repository.Verify(
            r => r.MarkOutboxDispatchFailedAsync(failing.OutboxId, "SQS unavailable", _options.MaxAttempts, It.IsAny<CancellationToken>()),
            Times.Once);

        _repository.Verify(
            r => r.DeleteDispatchedOutboxMessagesAsync(
                It.Is<IEnumerable<Guid>>(ids => ids.SequenceEqual(new[] { succeeding.OutboxId })),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenAllPublishesFail_DoesNotCallDelete()
    {
        var outbox = CreateOutbox();

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([outbox]);

        _publisher
            .Setup(p => p.PublishAsync(It.IsAny<ReportJobRequestedMessage>(), It.IsAny<FifoMessageMetadata>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var sut = CreateSut();

        await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

        _repository.Verify(
            r => r.DeleteDispatchedOutboxMessagesAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenPublishThrowsOperationCanceled_RethrowsAndStopsProcessingRemaining()
    {
        var first = CreateOutbox();
        var second = CreateOutbox();

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([first, second]);

        var cts = new CancellationTokenSource();

        _publisher
            .Setup(p => p.PublishAsync(
                It.Is<ReportJobRequestedMessage>(m => m.Id == first.OutboxId),
                It.IsAny<FifoMessageMetadata>(),
                It.IsAny<CancellationToken>()))
            .Returns(async () =>
            {
                await cts.CancelAsync();
                throw new OperationCanceledException(cts.Token);
            });

        var sut = CreateSut();

        var act = () => InvokeDispatchPendingAsync(sut, _options, cts.Token);

        await act.Should().ThrowAsync<OperationCanceledException>();

        // The second message must never be attempted once cancellation is observed,
        // and nothing gets deleted/marked-failed for the cancelled attempt.
        _publisher.Verify(
            p => p.PublishAsync(
                It.Is<ReportJobRequestedMessage>(m => m.Id == second.OutboxId),
                It.IsAny<FifoMessageMetadata>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _repository.Verify(
            r => r.MarkOutboxDispatchFailedAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _repository.Verify(
            r => r.DeleteDispatchedOutboxMessagesAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenCorrelationIdContextIsSet_UsesItAsMessageMetadataCorrelationId()
    {
        var outbox = CreateOutbox();

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([outbox]);

        CorrelationIdContext.Value = "existing-correlation-id";

        try
        {
            var sut = CreateSut();

            await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

            _publisher.Verify(
                p => p.PublishAsync(
                    It.IsAny<ReportJobRequestedMessage>(),
                    It.Is<FifoMessageMetadata>(m => m.CorrelationId == "existing-correlation-id"),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
        finally
        {
            CorrelationIdContext.Value = null;
        }
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenCorrelationIdContextIsNotSet_GeneratesANonEmptyCorrelationId()
    {
        var outbox = CreateOutbox();

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([outbox]);

        CorrelationIdContext.Value = null;

        var sut = CreateSut();

        await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

        _publisher.Verify(
            p => p.PublishAsync(
                It.IsAny<ReportJobRequestedMessage>(),
                It.Is<FifoMessageMetadata>(m => !string.IsNullOrWhiteSpace(m.CorrelationId)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // -----------------------------------------------------------------------
    // BuildMessage (private static JSON-payload parsing helper)
    // -----------------------------------------------------------------------

    [Fact]
    public void BuildMessage_WithFullPayload_UsesParsedJobIdAndReportKey()
    {
        var reportJobId = Guid.NewGuid();
        var outbox = CreateOutbox(Guid.NewGuid(), $$"""{"reportJobId":"{{reportJobId}}","reportKey":"gb_cattle_registrations"}""");

        var message = InvokeBuildMessage(outbox);

        message.Id.Should().Be(outbox.OutboxId);
        message.JobId.Should().Be(reportJobId);
        message.ReportKey.Should().Be("gb_cattle_registrations");
    }

    [Fact]
    public void BuildMessage_WithEmptyPayload_FallsBackToOutboxReportJobIdAndEmptyReportKey()
    {
        var outbox = CreateOutbox(payload: "{}");

        var message = InvokeBuildMessage(outbox);

        message.Id.Should().Be(outbox.OutboxId);
        message.JobId.Should().Be(outbox.ReportJobId);
        message.ReportKey.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithInvalidJsonPayload_FallsBackGracefullyWithoutThrowing()
    {
        var outbox = CreateOutbox(payload: "{not-valid-json");

        var message = InvokeBuildMessage(outbox);

        message.Id.Should().Be(outbox.OutboxId);
        message.JobId.Should().Be(outbox.ReportJobId);
        message.ReportKey.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithNonGuidJobIdInPayload_FallsBackToOutboxReportJobId()
    {
        var outbox = CreateOutbox(payload: """{"reportJobId":"not-a-guid","reportKey":"holdings_summary"}""");

        var message = InvokeBuildMessage(outbox);

        message.JobId.Should().Be(outbox.ReportJobId);
        message.ReportKey.Should().Be("holdings_summary");
    }

    // -----------------------------------------------------------------------
    // ExecuteAsync outer loop guard (invoked via reflection; cancellation during
    // Task.Delay is what the loop uses to exit cleanly)
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ExecuteAsync_WhenTokenAlreadyCancelled_NeverPolls()
    {
        var sut = CreateSut();

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await InvokeExecuteAsync(sut, cts.Token);

        _scopeFactory.Verify(x => x.CreateScope(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_WhenDispatchThrowsUnhandledException_LogsErrorAndKeepsLoopingUntilCancelled()
    {
        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("db unavailable"));

        _logger.Setup(l => l.IsEnabled(LogLevel.Error)).Returns(true);

        var sut = CreateSut();

        using var cts = new CancellationTokenSource();

        var task = InvokeExecuteAsync(sut, cts.Token);

        // Give the first iteration a chance to run, throw, and be logged.
        await Task.Delay(100, TestContext.Current.CancellationToken);
        await cts.CancelAsync();

        await task;

        _logger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);

        _repository.Verify(
            r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    private static Task InvokeDispatchPendingAsync(
        ReportJobOutboxDispatcher dispatcher,
        ReportJobOutboxDispatcherOptions options,
        CancellationToken cancellationToken)
    {
        var method = typeof(ReportJobOutboxDispatcher)
            .GetMethod("DispatchPendingAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        return (Task)method!.Invoke(dispatcher, [options, cancellationToken])!;
    }

    private static ReportJobRequestedMessage InvokeBuildMessage(MiReportJobOutbox outbox)
    {
        var method = typeof(ReportJobOutboxDispatcher)
            .GetMethod("BuildMessage", BindingFlags.NonPublic | BindingFlags.Static);

        return (ReportJobRequestedMessage)method!.Invoke(null, [outbox])!;
    }

    private static Task InvokeExecuteAsync(ReportJobOutboxDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var method = typeof(ReportJobOutboxDispatcher)
            .GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        return (Task)method!.Invoke(dispatcher, [cancellationToken])!;
    }
}





