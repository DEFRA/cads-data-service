using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Infrastructure.Messaging.Publishers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Messaging.Publishers;

public class MiBffFifoQueuePublisherTests
{
    private readonly Mock<IAmazonSQS> _sqsMock = new();
    private readonly Mock<IMessageFactory> _messageFactoryMock = new();
    private readonly Mock<IOptionsMonitor<QueuePublisherOptions>> _optionsMonitorMock = new();

    private readonly QueuePublisherOptions _queuePublisherOptions = new()
    {
        Name = "MiBffFifoQueueClient",
        QueueUrl = TestSqsConstants.TestQueueUrl
    };

    private readonly MiBffFifoQueueClient _client = new();

    private MiBffFifoQueuePublisher CreateSut()
    {
        _optionsMonitorMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(_queuePublisherOptions);

        return new MiBffFifoQueuePublisher(
            _sqsMock.Object,
            _messageFactoryMock.Object,
            _optionsMonitorMock.Object,
            _client);
    }

    [Fact]
    public void QueueUrl_ShouldReturnConfigurationValue_WhenCalled()
    {
        var sut = CreateSut();

        var result = sut.QueueUrl;

        result.Should().Be(TestSqsConstants.TestQueueUrl);
    }

    [Fact]
    public async Task PublishAsync_ShouldThrowArgumentException_WhenMessageIsNull()
    {
        var sut = CreateSut();
        object? message = null;

        var metadata = new FifoMessageMetadata("Group", "Dedup", "MyCorrelationId");
        var action = () => sut.PublishAsync(message, metadata);

        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Message payload was null (Parameter 'message')");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task PublishAsync_ShouldThrowPublishFailedException_WhenQueueUrlIsEmpty(string? queueUrl)
    {
        var message = new { Content = "Test message" };
        var sut = CreateSut();

        _optionsMonitorMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(new QueuePublisherOptions { Name = "MiBffFifoQueueClient", QueueUrl = queueUrl! });

        var metadata = new FifoMessageMetadata("Group", "Dedup", "MyCorrelationId");
        var action = () => sut.PublishAsync(message, metadata);

        await action.Should().ThrowAsync<PublishFailedException>()
            .WithMessage("QueueUrl is missing");
    }

    [Fact]
    public async Task PublishAsync_ShouldSendMessage_WhenValidMessageProvided()
    {
        var message = new { Content = "Test message" };
        var sendRequest = new SendMessageRequest
        {
            QueueUrl = TestSqsConstants.TestQueueUrl,
            MessageBody = "serialized message"
        };

        var metadata = new FifoMessageMetadata("Group", "Dedup", "Corr");

        var sut = CreateSut();

        _messageFactoryMock
            .Setup(x => x.CreateFifoSqsMessage(TestSqsConstants.TestQueueUrl, message, metadata, It.IsAny<string?>()))
            .Returns(sendRequest);

        _sqsMock
            .Setup(x => x.SendMessageAsync(sendRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SendMessageResponse { MessageId = "test-message-id" });

        await sut.PublishAsync(message, metadata, TestContext.Current.CancellationToken);

        _messageFactoryMock.Verify(
            x => x.CreateFifoSqsMessage(TestSqsConstants.TestQueueUrl, message, metadata, It.IsAny<string?>()),
            Times.Once);
        _sqsMock.Verify(x => x.SendMessageAsync(sendRequest, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task PublishAsync_ShouldThrowPublishFailedException_WhenSqsSendFails()
    {
        var message = new { Content = "Test message" };
        var sendRequest = new SendMessageRequest { QueueUrl = TestSqsConstants.TestQueueUrl, MessageBody = "serialized message" };
        var metadata = new FifoMessageMetadata("Group", "Dedup", "MyCorrelationId");

        var sut = CreateSut();

        _messageFactoryMock
            .Setup(x => x.CreateFifoSqsMessage(TestSqsConstants.TestQueueUrl, message, metadata, It.IsAny<string?>()))
            .Returns(sendRequest);

        _sqsMock
            .Setup(x => x.SendMessageAsync(sendRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("SQS service unavailable"));

        var action = () => sut.PublishAsync(message, metadata, TestContext.Current.CancellationToken);

        await action.Should().ThrowAsync<PublishFailedException>()
            .WithMessage($"Failed to publish message on {TestSqsConstants.TestQueueUrl}.");
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task PublishAsync_ShouldMarkExceptionAsTransient_WhenAmazonSqsExceptionHasRetryableStatusCode(HttpStatusCode statusCode)
    {
        var message = new { Content = "Test message" };
        var sendRequest = new SendMessageRequest { QueueUrl = TestSqsConstants.TestQueueUrl, MessageBody = "serialized message" };
        var metadata = new FifoMessageMetadata("Group", "Dedup", "MyCorrelationId");

        var sut = CreateSut();

        _messageFactoryMock
            .Setup(x => x.CreateFifoSqsMessage(TestSqsConstants.TestQueueUrl, message, metadata, It.IsAny<string?>()))
            .Returns(sendRequest);

        _sqsMock
            .Setup(x => x.SendMessageAsync(sendRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonSQSException("service unavailable") { StatusCode = statusCode });

        var act = () => sut.PublishAsync(message, metadata, TestContext.Current.CancellationToken);

        var thrown = await act.Should().ThrowAsync<PublishFailedException>();
        thrown.Which.IsTransient.Should().BeTrue();
    }

    [Fact]
    public async Task PublishAsync_ShouldMarkExceptionAsNonTransient_WhenAmazonSqsExceptionHasNonRetryableStatusCode()
    {
        var message = new { Content = "Test message" };
        var sendRequest = new SendMessageRequest { QueueUrl = TestSqsConstants.TestQueueUrl, MessageBody = "serialized message" };
        var metadata = new FifoMessageMetadata("Group", "Dedup", "MyCorrelationId");

        var sut = CreateSut();

        _messageFactoryMock
            .Setup(x => x.CreateFifoSqsMessage(TestSqsConstants.TestQueueUrl, message, metadata, It.IsAny<string?>()))
            .Returns(sendRequest);

        _sqsMock
            .Setup(x => x.SendMessageAsync(sendRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonSQSException("bad request") { StatusCode = HttpStatusCode.BadRequest });

        var act = () => sut.PublishAsync(message, metadata, TestContext.Current.CancellationToken);

        var thrown = await act.Should().ThrowAsync<PublishFailedException>();
        thrown.Which.IsTransient.Should().BeFalse();
    }

    [Fact]
    public async Task PublishAsync_ShouldMarkExceptionAsNonTransient_WhenExceptionIsNotAnAmazonSqsException()
    {
        var message = new { Content = "Test message" };
        var sendRequest = new SendMessageRequest { QueueUrl = TestSqsConstants.TestQueueUrl, MessageBody = "serialized message" };
        var metadata = new FifoMessageMetadata("Group", "Dedup", "MyCorrelationId");

        var sut = CreateSut();

        _messageFactoryMock
            .Setup(x => x.CreateFifoSqsMessage(TestSqsConstants.TestQueueUrl, message, metadata, It.IsAny<string?>()))
            .Returns(sendRequest);

        _sqsMock
            .Setup(x => x.SendMessageAsync(sendRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var act = () => sut.PublishAsync(message, metadata, TestContext.Current.CancellationToken);

        var thrown = await act.Should().ThrowAsync<PublishFailedException>();
        thrown.Which.IsTransient.Should().BeFalse();
    }
}