using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Consumers;

public class BaseSqsQueuePollerTests
{
    private readonly Mock<IServiceScopeFactory> _scopeFactoryMock = new();
    private readonly Mock<IAmazonSQS> _sqsMock = new();
    private readonly TestMessageCommandRegistry _messageCommandRegistry = new();
    private readonly Mock<IOptionsMonitor<QueueConsumerOptions>> _optionsMonitorMock = new();
    private readonly Mock<IQueueAdminService<TestFifoQueueClient>> _queueAdminServiceMock = new();
    private readonly Mock<IQueuePollerObserver<MessageType>> _observerMock = new();
    private readonly Mock<ILogger<TestFifoQueuePoller>> _loggerMock = new();

    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<IServiceProvider> _serviceProviderMock = new();
    private readonly Mock<IServiceScope> _scopeMock = new();

    private readonly TestFifoQueueClient _testFifoQueueClient = new();

    private readonly QueueConsumerOptions _queueConsumerOptions = new()
    {
        Name = "TestQueue",
        QueueUrl = $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue",
        WaitTimeSeconds = 5,
        MaxNumberOfMessages = 10
    };

    private TestFifoQueuePoller CreateSut()
    {
        _messageCommandRegistry.Register<TestImportMessageCommandFactory>("TestImport");

        _optionsMonitorMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(_queueConsumerOptions);

        _loggerMock.EnableAllLogLevels();

        return new TestFifoQueuePoller(
            _scopeFactoryMock.Object,
            _sqsMock.Object,
            _messageCommandRegistry,
            _optionsMonitorMock.Object,
            _testFifoQueueClient,
            _queueAdminServiceMock.Object,
            _observerMock.Object,
            _loggerMock.Object);
    }

    // -------------------------------------------------------------------------
    // Basic Start/Stop/Cancel
    // -------------------------------------------------------------------------

    [Fact]
    public async Task StartAsync_ShouldLogAndStartPolling_WhenEnabled()
    {
        var sut = CreateSut();

        await sut.StartAsync(CancellationToken.None);

        _loggerMock.Verify(x =>
            x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains($"QueuePoller TClient: {typeof(TestFifoQueueClient).Name} start requested.")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task StopAsync_ShouldLogAndCancelPolling()
    {
        var sut = CreateSut();
        await sut.StartAsync(CancellationToken.None);

        await sut.StopAsync(CancellationToken.None);

        _loggerMock.Verify(x =>
            x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains($"QueuePoller TClient: {typeof(TestFifoQueueClient).Name} stop requested.")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task PollMessagesAsync_ShouldHandleOperationCanceledException_Gracefully()
    {
        var cancellationSource = new CancellationTokenSource();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .Returns(async (ReceiveMessageRequest _, CancellationToken token) =>
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();
                return new ReceiveMessageResponse { Messages = [] };
            });

        var sut = CreateSut();

        await sut.StartAsync(cancellationSource.Token);

        await Task.Delay(50, TestContext.Current.CancellationToken);
        cancellationSource.Cancel();

        await sut.StopAsync(CancellationToken.None);

        _loggerMock.Verify(x =>
            x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains($"QueuePoller TClient: {typeof(TestFifoQueueClient).Name} stop requested.")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

        Func<Task> act = async () => await sut.DisposeAsync();
        await act.Should().NotThrowAsync();
    }

    // -------------------------------------------------------------------------
    // Happy Path
    // -------------------------------------------------------------------------

    [Fact]
    public async Task PollMessagesAsync_ShouldProcessMessages()
    {
        var testImportMessage = new TestImportMessage { Identifier = Guid.NewGuid().ToString() };
        var payload = JsonSerializer.Serialize(testImportMessage, JsonDefaults.DefaultOptionsWithStringEnumConversion);
        var messageHandled = new TaskCompletionSource();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .Returns(async (ReceiveMessageRequest _, CancellationToken token) =>
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();
                return GetReceiveMessageResponseArgs(payload);
            });

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TestImportMessageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(testImportMessage)
            .Callback(() => messageHandled.TrySetResult());

        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IMediator))).Returns(_mediatorMock.Object);
        _scopeMock.Setup(s => s.ServiceProvider).Returns(_serviceProviderMock.Object);
        _scopeFactoryMock.Setup(f => f.CreateScope()).Returns(_scopeMock.Object);

        await RunUntilObserved(messageHandled);

        _sqsMock.Verify(x => x.DeleteMessageAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

        _observerMock.Verify(x => x.OnMessageHandled(
            It.IsAny<string>(), It.IsAny<DateTime>(),
            It.IsAny<MessageType?>(), It.IsAny<Message>()), Times.Once);
    }

    // -------------------------------------------------------------------------
    // RetryableException
    // -------------------------------------------------------------------------

    [Fact]
    public async Task HandleMessageAsync_WhenRetryableException_CallsOnMessageFailed()
    {
        var observed = SetupExceptionScenario(new RetryableException("temporary"));
        await RunUntilObserved(observed);

        _observerMock.Verify(o => o.OnMessageFailed(
            It.IsAny<string>(), It.IsAny<DateTime>(),
            It.IsAny<RetryableException>(), It.IsAny<Message>()), Times.Once);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenRetryableException_DoesNotDeleteMessage()
    {
        var observed = SetupExceptionScenario(new RetryableException("temporary"));
        await RunUntilObserved(observed);

        _sqsMock.Verify(x => x.DeleteMessageAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenRetryableException_DoesNotMoveToDeadLetterQueue()
    {
        var observed = SetupExceptionScenario(new RetryableException("temporary"));
        await RunUntilObserved(observed);

        _queueAdminServiceMock.Verify(x => x.MoveToDeadLetterQueueAsync(
            It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
            It.IsAny<Exception>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenRetryableException_LogsWarning()
    {
        var observed = SetupExceptionScenario(new RetryableException("temporary"));
        await RunUntilObserved(observed);

        _loggerMock.Verify(x => x.Log(
            LogLevel.Warning,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("RetryableException")),
            It.IsAny<RetryableException>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
    }

    // -------------------------------------------------------------------------
    // NonRetryableException
    // -------------------------------------------------------------------------

    [Fact]
    public async Task HandleMessageAsync_WhenNonRetryableException_CallsOnMessageFailed()
    {
        var observed = SetupExceptionScenario(new NonRetryableException("permanent"));
        await RunUntilObserved(observed);

        _observerMock.Verify(o => o.OnMessageFailed(
            It.IsAny<string>(), It.IsAny<DateTime>(),
            It.IsAny<NonRetryableException>(), It.IsAny<Message>()), Times.Once);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenNonRetryableException_DoesNotDeleteMessage()
    {
        var observed = SetupExceptionScenario(new NonRetryableException("permanent"));
        await RunUntilObserved(observed);

        _sqsMock.Verify(x => x.DeleteMessageAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenNonRetryableException_MovesToDeadLetterQueue()
    {
        var observed = SetupExceptionScenario(new NonRetryableException("permanent"));
        await RunUntilObserved(observed);

        _queueAdminServiceMock.Verify(x => x.MoveToDeadLetterQueueAsync(
            It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
            It.IsAny<NonRetryableException>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenNonRetryableException_LogsError()
    {
        var observed = SetupExceptionScenario(new NonRetryableException("permanent"));
        await RunUntilObserved(observed);

        _loggerMock.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("NonRetryableException")),
            It.IsAny<NonRetryableException>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
    }

    // -------------------------------------------------------------------------
    // Unexpected exception
    // -------------------------------------------------------------------------

    [Fact]
    public async Task HandleMessageAsync_WhenUnexpectedException_CallsOnMessageFailed()
    {
        var observed = SetupExceptionScenario(new InvalidOperationException("unexpected"));
        await RunUntilObserved(observed);

        _observerMock.Verify(o => o.OnMessageFailed(
            It.IsAny<string>(), It.IsAny<DateTime>(),
            It.IsAny<InvalidOperationException>(), It.IsAny<Message>()), Times.Once);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenUnexpectedException_DoesNotDeleteMessage()
    {
        var observed = SetupExceptionScenario(new InvalidOperationException("unexpected"));
        await RunUntilObserved(observed);

        _sqsMock.Verify(x => x.DeleteMessageAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenUnexpectedException_MovesToDeadLetterQueue()
    {
        var observed = SetupExceptionScenario(new InvalidOperationException("unexpected"));
        await RunUntilObserved(observed);

        _queueAdminServiceMock.Verify(x => x.MoveToDeadLetterQueueAsync(
            It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
            It.IsAny<InvalidOperationException>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenUnexpectedException_LogsError()
    {
        var observed = SetupExceptionScenario(new InvalidOperationException("unexpected"));
        await RunUntilObserved(observed);

        _loggerMock.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("UnhandledException")),
            It.IsAny<InvalidOperationException>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
    }

    // -------------------------------------------------------------------------
    // No command registered for subject
    // -------------------------------------------------------------------------

    [Fact]
    public async Task HandleMessageAsync_WhenNoCommandRegisteredForSubject_MovesToDeadLetterQueue()
    {
        // Bypass the mediator entirely — the registry throws before Send is called
        var observed = new TaskCompletionSource();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .Returns(async (ReceiveMessageRequest _, CancellationToken token) =>
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();
                return new ReceiveMessageResponse
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Messages = [BuildSqsMessage(
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    "UnregisteredSubject",
                    "{}")]
                };
            });

        _queueAdminServiceMock
            .Setup(x => x.MoveToDeadLetterQueueAsync(
                It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
                It.IsAny<Exception>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _observerMock
            .Setup(o => o.OnMessageFailed(
                It.IsAny<string>(), It.IsAny<DateTime>(),
                It.IsAny<Exception>(), It.IsAny<Message>()))
            .Callback(() => observed.TrySetResult());

        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IMediator))).Returns(_mediatorMock.Object);
        _scopeMock.Setup(s => s.ServiceProvider).Returns(_serviceProviderMock.Object);
        _scopeFactoryMock.Setup(f => f.CreateScope()).Returns(_scopeMock.Object);

        await RunUntilObserved(observed);

        _queueAdminServiceMock.Verify(x => x.MoveToDeadLetterQueueAsync(
            It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
            It.IsAny<InvalidOperationException>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleMessageAsync_WhenNoCommandRegisteredForSubject_DoesNotDeleteMessage()
    {
        var observed = new TaskCompletionSource();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .Returns(async (ReceiveMessageRequest _, CancellationToken token) =>
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();
                return new ReceiveMessageResponse
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Messages = [BuildSqsMessage(
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    "UnregisteredSubject",
                    "{}")]
                };
            });

        _queueAdminServiceMock
            .Setup(x => x.MoveToDeadLetterQueueAsync(
                It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
                It.IsAny<Exception>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _observerMock
            .Setup(o => o.OnMessageFailed(
                It.IsAny<string>(), It.IsAny<DateTime>(),
                It.IsAny<Exception>(), It.IsAny<Message>()))
            .Callback(() => observed.TrySetResult());

        await RunUntilObserved(observed);

        _sqsMock.Verify(x => x.DeleteMessageAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    public class TestFifoQueuePoller(
        IServiceScopeFactory scopeFactory,
        IAmazonSQS sqs,
        TestMessageCommandRegistry registry,
        IOptionsMonitor<QueueConsumerOptions> options,
        TestFifoQueueClient client,
        IQueueAdminService<TestFifoQueueClient> queueAdminService,
        IQueuePollerObserver<MessageType> observer,
        ILogger<TestFifoQueuePoller> logger)
        : BaseSqsQueuePoller<TestFifoQueueClient>(scopeFactory, sqs, options, client, queueAdminService, observer, logger)
    {
        protected override async Task<MessageType?> ProcessMessageAsync(
            UnwrappedMessage message,
            CancellationToken cancellationToken)
        {
            using var scope = ScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var command = registry.CreateCommand(message);
            return await mediator.Send(command, cancellationToken);
        }
    }

    public class TestFifoQueueClient : IQueueClient
    {
        public string ClientName => GetType().Name;
    }

    public sealed class TestMessageCommandRegistry
    {
        private readonly Dictionary<string, IMessageCommandFactory> _map = [];

        public void Register<TFactory>(string subject)
            where TFactory : IMessageCommandFactory, new()
        {
            _map[subject] = new TFactory();
        }

        public IMessageProcessingCommand CreateCommand(UnwrappedMessage message)
        {
            if (!_map.TryGetValue(message.Subject, out var factory))
                throw new InvalidOperationException($"No command registered for subject {message.Subject}");

            return factory.Create(message);
        }
    }

    public class TestImportMessage : MessageType
    {
        public string Identifier { get; set; } = string.Empty;
    }

    public sealed class TestImportMessageCommandFactory : IMessageCommandFactory
    {
        public IMessageProcessingCommand Create(UnwrappedMessage message)
            => new TestImportMessageCommand(message);
    }

    public sealed record TestImportMessageCommand(UnwrappedMessage Message)
        : IMessageProcessingCommand;

    private static ReceiveMessageResponse GetReceiveMessageResponseArgs(string payload)
    {
        var messageArgs = BuildSqsMessage(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), typeof(TestImportMessage).Name, payload);
        var receiveMessageResponseArgs = new ReceiveMessageResponse { HttpStatusCode = System.Net.HttpStatusCode.OK, Messages = [messageArgs] };
        return receiveMessageResponseArgs;
    }

    private static Message BuildSqsMessage(string messageId, string correlationId, string subject, string payload)
    {
        return new Message
        {
            MessageId = messageId,
            Body = payload,
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                ["Subject"] = new() { DataType = "String", StringValue = subject },
                ["CorrelationId"] = new() { DataType = "String", StringValue = correlationId }
            }
        };
    }

    private TaskCompletionSource SetupExceptionScenario(Exception exceptionToThrow)
    {
        var observed = new TaskCompletionSource();

        var payload = JsonSerializer.Serialize(
            new TestImportMessage { Identifier = Guid.NewGuid().ToString() },
            JsonDefaults.DefaultOptionsWithStringEnumConversion);

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .Returns(async (ReceiveMessageRequest _, CancellationToken token) =>
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();
                return GetReceiveMessageResponseArgs(payload);
            });

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TestImportMessageCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exceptionToThrow);

        _queueAdminServiceMock
            .Setup(x => x.MoveToDeadLetterQueueAsync(
                It.IsAny<Message>(), It.IsAny<string>(), It.IsAny<string?>(),
                It.IsAny<Exception>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _observerMock
            .Setup(o => o.OnMessageFailed(
                It.IsAny<string>(), It.IsAny<DateTime>(),
                It.IsAny<Exception>(), It.IsAny<Message>()))
            .Callback(() => observed.TrySetResult());

        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IMediator))).Returns(_mediatorMock.Object);
        _scopeMock.Setup(s => s.ServiceProvider).Returns(_serviceProviderMock.Object);
        _scopeFactoryMock.Setup(f => f.CreateScope()).Returns(_scopeMock.Object);

        return observed;
    }

    private async Task RunUntilObserved(TaskCompletionSource observed)
    {
        var sut = CreateSut();
        using var cts = new CancellationTokenSource();

        await sut.StartAsync(cts.Token);
        await observed.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
        await Task.Delay(50, TestContext.Current.CancellationToken);
        await cts.CancelAsync();
        await sut.StopAsync(CancellationToken.None);
        await sut.DisposeAsync();
    }
}