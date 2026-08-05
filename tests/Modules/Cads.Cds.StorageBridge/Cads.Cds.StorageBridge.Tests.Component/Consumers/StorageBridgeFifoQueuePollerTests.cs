using Amazon.SQS.Model;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.ApiSurface.Messages.Imports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Testing.Support.TestDoubles.Observers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Messaging;
using Cads.Cds.StorageBridge.Application.Messaging.Clients;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;

namespace Cads.Cds.StorageBridge.Tests.Component.Consumers;

public class StorageBridgeFifoQueuePollerTests(StorageBridgeTestFixture testFixture) : IClassFixture<StorageBridgeTestFixture>
{
    private readonly StorageBridgeTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenProcessingMessage_WhenNoMessageHandlerIsRegistered_ThenShouldCallOnMessageFailed()
    {
        var messageId = Guid.NewGuid().ToString();
        var metadata = GetFifoMessageMetadata();

        var message = new NonRegisteredTestMessage();
        var messageArgs = GetMessageWithOriginSqsArgs(messageId, message, metadata);
        var receiveMessageResponseArgs = GetReceiveMessageResponseArgs(messageArgs);

        _testFixture.Factory.AmazonSQSMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK, Messages = [] });
        _testFixture.Factory.AmazonSQSMock
            .SetupSequence(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(receiveMessageResponseArgs)
            .ReturnsAsync(new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK, Messages = [] });

        await using var scope = _testFixture.Factory.Services.CreateAsyncScope();
        var queuePoller = scope.ServiceProvider.GetRequiredService<IQueuePoller<StorageBridgeFifoQueueClient>>();
        var queuePollerObserver = scope.ServiceProvider.GetRequiredService<TestQueuePollerObserver<MessageType>>();

        using var cts = new CancellationTokenSource();
        await queuePoller.StartAsync(cts.Token);

        var (MessageId, Exception) = await queuePollerObserver.MessageFailed;

        MessageId.Should().NotBeNull().And.Be(messageId);
        Exception.Should().BeOfType<InvalidOperationException>();
        Exception.Message.Should().Be($"No command registered for subject NonRegisteredTest");
    }

    [Fact]
    public async Task GivenProcessingMessage_WhenMessageHandlerSucceeds_ShouldCompleteMessage()
    {
        var messageId = Guid.NewGuid().ToString();
        var metadata = GetFifoMessageMetadata();

        var fileImportId = 1;
        var objectKey = Guid.NewGuid().ToString();
        var message = GetMessage(fileImportId, objectKey);
        var messageArgs = GetMessageWithOriginSqsArgs(messageId, message, metadata);
        var receiveMessageResponseArgs = GetReceiveMessageResponseArgs(messageArgs);

        _testFixture.Factory.AmazonSQSMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK, Messages = [] });
        _testFixture.Factory.AmazonSQSMock
            .SetupSequence(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(receiveMessageResponseArgs)
            .ReturnsAsync(new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK, Messages = [] });
        _testFixture.Factory.AmazonSQSMock
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteMessageResponse { HttpStatusCode = HttpStatusCode.OK });

        await using var scope = _testFixture.Factory.Services.CreateAsyncScope();
        var queuePoller = scope.ServiceProvider.GetRequiredService<IQueuePoller<StorageBridgeFifoQueueClient>>();
        var queuePollerObserver = scope.ServiceProvider.GetRequiredService<TestQueuePollerObserver<MessageType>>();

        using var cts = new CancellationTokenSource();
        await queuePoller.StartAsync(cts.Token);

        var (MessageId, Payload) = await queuePollerObserver.MessageHandled;
        var payloadAsType = Payload as S3ToPostgresCopyMessage;

        MessageId.Should().NotBeNull().And.Be(messageId);
        payloadAsType.Should().NotBeNull();
        payloadAsType.FileImportId.Should().Be(fileImportId);
        payloadAsType.ObjectKey.Should().Be(objectKey);

        SqsMessageUtility.VerifyMessageWasCompleted(_testFixture.Factory.AmazonSQSMock);
    }

    private static S3ToPostgresCopyMessage GetMessage(long fileImportId, string objectKey) => new()
    {
        FileImportId = fileImportId,
        ObjectKey = objectKey
    };

    private static ReceiveMessageResponse GetReceiveMessageResponseArgs(Message message)
    {
        var receiveMessageResponse = SqsMessageUtility.CreateReceiveMessageResponse(message);
        return receiveMessageResponse;
    }

    private static Message GetMessageWithOriginSqsArgs<TMessage>(string messageId, TMessage placeholderMessage, FifoMessageMetadata metadata)
    {
        var message = SqsMessageUtility.CreateFifoSqsMessage<TMessage>(messageId, placeholderMessage, metadata, typeof(TMessage).Name);
        return message;
    }

    private static FifoMessageMetadata GetFifoMessageMetadata()
    {
        return new FifoMessageMetadata(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString());
    }

    public class NonRegisteredTestMessage : MessageType { }
}