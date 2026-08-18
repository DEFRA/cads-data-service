using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Extensions;
using Moq;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Messaging;

public static class SqsMessageUtility
{
    public static ReceiveMessageResponse CreateReceiveMessageResponse(Message message)
    {
        var receiveMessageResponse = new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK, Messages = [message] };
        return receiveMessageResponse;
    }

    public static ReceiveMessageResponse CreateReceiveMessageResponse(List<Message> messages)
    {
        var receiveMessageResponse = new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK, Messages = messages };
        return receiveMessageResponse;
    }

    public static Message CreateFifoSqsMessage<TMessage>(string messageId, TMessage message, FifoMessageMetadata metadata, string? subject = null)
    {
        var messageSerialized = JsonSerializer.Serialize(message, JsonDefaults.DefaultOptionsWithStringEnumConversion);
        var serviceBusMessage = new Message { MessageId = messageId, ReceiptHandle = messageId, Body = messageSerialized, MessageAttributes = [] };

        serviceBusMessage.MessageAttributes.TryAdd("Subject", new MessageAttributeValue() { DataType = "String", StringValue = (subject ?? typeof(TMessage).Name).ReplaceSuffix() });
        serviceBusMessage.MessageAttributes.TryAdd("CorrelationId", new MessageAttributeValue() { DataType = "String", StringValue = metadata.CorrelationId });
        serviceBusMessage.MessageAttributes.TryAdd("MessageGroupId", new MessageAttributeValue() { DataType = "String", StringValue = metadata.MessageGroupId });
        serviceBusMessage.MessageAttributes.TryAdd("MessageDeduplicationId", new MessageAttributeValue() { DataType = "String", StringValue = metadata.MessageDeduplicationId });

        return serviceBusMessage;
    }

    public static SendMessageRequest CreateFifoSqsMessage<TMessage>(
        string queueUrl,
        TMessage message,
        string? subject = null,
        string? correlationId = null,
        string? messageGroupId = null,
        string? messageDeduplicationId = null)
    {
        var body = JsonSerializer.Serialize(message, JsonDefaults.DefaultOptionsWithStringEnumConversion);

        return new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = body,
            MessageGroupId = messageGroupId ?? Guid.NewGuid().ToString(),
            MessageDeduplicationId = messageDeduplicationId ?? Guid.NewGuid().ToString(),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                ["Subject"] = new() { DataType = "String", StringValue = (subject ?? typeof(TMessage).Name).ReplaceSuffix() },
                ["CorrelationId"] = new() { DataType = "String", StringValue = correlationId ?? Guid.NewGuid().ToString() }
            }
        };
    }

    public static void VerifyMessageWasCompleted(Mock<IAmazonSQS>? sqsMock)
    {
        sqsMock?.Verify(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}