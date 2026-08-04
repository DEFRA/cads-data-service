using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
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

    [Fact]
    public async Task PollMessagesAsync_ShouldProcessMessages()
    {
        var cancellationSource = new CancellationTokenSource();

        var testImportMessage = new TestImportMessage { Identifier = Guid.NewGuid().ToString() };
        var testImportMessageSerialized = JsonSerializer.Serialize(testImportMessage, JsonDefaults.DefaultOptionsWithStringEnumConversion);

        var messageHandled = new TaskCompletionSource();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .Returns(async (ReceiveMessageRequest _, CancellationToken token) =>
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();
                return GetReceiveMessageResponseArgs(testImportMessageSerialized);
            });

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<TestImportMessageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(testImportMessage)
            .Callback(() => messageHandled.TrySetResult());

        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IMediator)))
            .Returns(_mediatorMock.Object);

        _scopeMock
            .Setup(s => s.ServiceProvider)
            .Returns(_serviceProviderMock.Object);

        _scopeFactoryMock
            .Setup(f => f.CreateScope())
            .Returns(_scopeMock.Object);

        var sut = CreateSut();

        await sut.StartAsync(cancellationSource.Token);

        await messageHandled.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);

        await Task.Delay(50, TestContext.Current.CancellationToken);
        cancellationSource.Cancel();

        await sut.StopAsync(CancellationToken.None);

        _sqsMock.Verify(x => x.DeleteMessageAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<CancellationToken>()),
            Times.Once);

        _observerMock.Verify(x => x.OnMessageHandled(
            It.IsAny<string>(),
            It.IsAny<DateTime>(),
            It.IsAny<MessageType?>(),
            It.IsAny<Message>()),
            Times.Once);

        await sut.DisposeAsync();
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
}