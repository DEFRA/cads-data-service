using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using Cads.Cds.BuildingBlocks.Core.Domain.Outbox;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Reflection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Dispatchers;

public class BaseOutboxDispatcherTests
{
    private readonly Mock<IServiceScopeFactory> _scopeFactory = new();
    private readonly Mock<IServiceScope> _scope = new();
    private readonly Mock<IServiceProvider> _provider = new();
    private readonly Mock<IOutboxRepository<TestOutbox>> _repository = new();
    private readonly Mock<IMessagePublisher<TestFifoQueueClient>> _publisher = new();
    private readonly Mock<IOptionsMonitor<OutboxDispatcherOptions>> _optionsMonitor = new();
    private readonly Mock<ILogger<TestOutboxDispatcher>> _logger = new();

    private readonly OutboxDispatcherOptions _options = new()
    {
        PollIntervalSeconds = 1,
        BatchSize = 20,
        MaxAttempts = 5
    };

    public BaseOutboxDispatcherTests()
    {
        _scopeFactory.Setup(x => x.CreateScope()).Returns(_scope.Object);
        _scope.Setup(x => x.ServiceProvider).Returns(_provider.Object);
        _provider.Setup(x => x.GetService(typeof(IOutboxRepository<TestOutbox>))).Returns(_repository.Object);
        _optionsMonitor.Setup(x => x.CurrentValue).Returns(_options);
    }

    private TestOutboxDispatcher CreateSut()
        => new(_scopeFactory.Object, _publisher.Object, _optionsMonitor.Object, _logger.Object);

    private static TestOutbox CreateOutbox() => new() { OutboxId = Guid.NewGuid() };

    // -----------------------------------------------------------------------
    // DispatchPendingAsync (invoked directly via reflection - the protected
    // method that ExecuteAsync's polling loop delegates to on each tick)
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
            p => p.PublishAsync(It.IsAny<TestMessage>(), It.IsAny<FifoMessageMetadata>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _repository.Verify(
            r => r.DeleteDispatchedOutboxMessagesAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DispatchPendingAsync_WhenMessagesClaimed_PublishesEachAndDeletesAllDispatched()
    {
        var outbox1 = CreateOutbox();
        var outbox2 = CreateOutbox();

        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([outbox1, outbox2]);

        var sut = CreateSut();

        await InvokeDispatchPendingAsync(sut, _options, TestContext.Current.CancellationToken);

        _publisher.Verify(
            p => p.PublishAsync(
                It.Is<TestMessage>(m => m.OutboxId == outbox1.OutboxId),
                It.IsAny<FifoMessageMetadata>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _publisher.Verify(
            p => p.PublishAsync(
                It.Is<TestMessage>(m => m.OutboxId == outbox2.OutboxId),
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
                It.Is<TestMessage>(m => m.OutboxId == failing.OutboxId),
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
            .Setup(p => p.PublishAsync(It.IsAny<TestMessage>(), It.IsAny<FifoMessageMetadata>(), It.IsAny<CancellationToken>()))
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
                It.Is<TestMessage>(m => m.OutboxId == first.OutboxId),
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

        _publisher.Verify(
            p => p.PublishAsync(
                It.Is<TestMessage>(m => m.OutboxId == second.OutboxId),
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

    // -----------------------------------------------------------------------
    // ExecuteAsync (the real PeriodicTimer polling loop) - driven via the
    // public StartAsync/StopAsync inherited from BackgroundService, exactly
    // as a component test would drive IOutboxDispatcher<TClient>.
    // -----------------------------------------------------------------------

    [Fact]
    public async Task StartAsync_WhenTokenAlreadyCancelled_NeverPolls()
    {
        var sut = CreateSut();

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await sut.StartAsync(cts.Token);
        await sut.StopAsync(CancellationToken.None);

        _scopeFactory.Verify(x => x.CreateScope(), Times.Never);
    }

    [Fact]
    public async Task StartAsync_WhenDispatchThrowsUnhandledException_LogsErrorAndKeepsLoopingUntilStopped()
    {
        _repository
            .Setup(r => r.ClaimPendingOutboxMessagesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("db unavailable"));

        _logger.Setup(l => l.IsEnabled(LogLevel.Error)).Returns(true);

        var sut = CreateSut();

        using var cts = new CancellationTokenSource();

        await sut.StartAsync(cts.Token);

        // Give the first tick (PollIntervalSeconds = 1) a chance to run, throw and be logged.
        await Task.Delay(TimeSpan.FromSeconds(1.5), TestContext.Current.CancellationToken);

        await sut.StopAsync(CancellationToken.None);

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
        TestOutboxDispatcher dispatcher,
        OutboxDispatcherOptions options,
        CancellationToken cancellationToken)
    {
        var method = typeof(TestOutboxDispatcher)
            .GetMethod("DispatchPendingAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        return (Task)method!.Invoke(dispatcher, [options, cancellationToken])!;
    }

    // -----------------------------------------------------------------------
    // Test doubles
    // -----------------------------------------------------------------------

    public class TestOutboxDispatcher(
        IServiceScopeFactory scopeFactory,
        IMessagePublisher<TestFifoQueueClient> publisher,
        IOptionsMonitor<OutboxDispatcherOptions> options,
        ILogger<TestOutboxDispatcher> logger)
        : BaseOutboxDispatcher<TestFifoQueueClient, TestOutbox>(
            scopeFactory, publisher, options, logger)
    {
        protected override Task PublishAsync(TestOutbox outboxMessage, CancellationToken cancellationToken)
        {
            var message = new TestMessage { OutboxId = outboxMessage.OutboxId };
            var metadata = new FifoMessageMetadata(
                outboxMessage.OutboxId.ToString(),
                outboxMessage.OutboxId.ToString(),
                Guid.NewGuid().ToString());

            return Publisher.PublishAsync(message, metadata, cancellationToken);
        }
    }

    public class TestOutbox : IOutboxMessage
    {
        public Guid OutboxId { get; init; }
    }

    public class TestMessage
    {
        public Guid OutboxId { get; init; }
    }


    public class TestFifoQueueClient : IQueueClient
    {
        public string ClientName => GetType().Name;
    }
}