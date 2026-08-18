using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Application.Messaging.Constants;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Messaging.Services;

public class StorageBridgeFifoQueueAdminServiceTests
{
    private readonly Mock<IAmazonSQS> _sqs = new();
    private readonly Mock<ILogger<StorageBridgeFifoQueueAdminService>> _logger = new();
    private readonly StorageBridgeFifoQueueAdminService _sut;

    private static string QueueUrl => TestSqsConstants.TestQueueUrl;
    private static string DlqUrl => TestSqsConstants.TestQueueUrl;

    public StorageBridgeFifoQueueAdminServiceTests()
    {
        _sut = new StorageBridgeFifoQueueAdminService(_sqs.Object, _logger.Object);

        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SendMessageResponse { MessageId = "dlq-message-id" });

        _sqs
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteMessageResponse());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task MoveToDeadLetterQueueAsync_WhenDlqUrlNullOrWhitespace_ReturnsFalse(string? dlqUrl)
    {
        var result = await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, dlqUrl, new Exception(), CancellationToken.None);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task MoveToDeadLetterQueueAsync_WhenDlqUrlNullOrWhitespace_LogsWarning(string? dlqUrl)
    {
        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, dlqUrl, new Exception(), CancellationToken.None);

        VerifyLogLevel(LogLevel.Warning, Times.Once());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task MoveToDeadLetterQueueAsync_WhenDlqUrlNullOrWhitespace_MakesNoSqsCalls(string? dlqUrl)
    {
        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, dlqUrl, new Exception(), CancellationToken.None);

        _sqs.Verify(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        _sqs.Verify(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_HappyPath_ReturnsTrue()
    {
        var result = await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        Assert.True(result);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_HappyPath_SendsMessageBodyToDlq()
    {
        const string body = "{\"original\":\"body\"}";
        var captured = await CallAndCaptureRequest(message: CreateMessage(body: body));

        Assert.Equal(DlqUrl, captured.QueueUrl);
        Assert.Equal(body, captured.MessageBody);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_HappyPath_DeletesOriginalMessageWithCorrectReceiptHandle()
    {
        const string receiptHandle = "unique-receipt-handle";

        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(receiptHandle: receiptHandle), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        _sqs.Verify(x => x.DeleteMessageAsync(QueueUrl, receiptHandle, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_HappyPath_DeleteOccursAfterSend()
    {
        var order = new List<string>();

        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .Callback<SendMessageRequest, CancellationToken>((_, _) => order.Add("send"))
            .ReturnsAsync(new SendMessageResponse { MessageId = "dlq-id" });

        _sqs
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Callback<string, string, CancellationToken>((_, _, _) => order.Add("delete"))
            .ReturnsAsync(new DeleteMessageResponse());

        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        Assert.Equal(["send", "delete"], order);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_SetsFailureReasonToExceptionTypeName()
    {
        var ex = new InvalidOperationException("something went wrong");
        var captured = await CallAndCaptureRequest(ex: ex);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqFailureReason];
        Assert.Equal(nameof(InvalidOperationException), attr.StringValue);
        Assert.Equal(DeadLetterQueueServiceConstants.StringDataType, attr.DataType);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_SetsFailureMessageToExceptionMessage()
    {
        var ex = new Exception("detailed failure message");
        var captured = await CallAndCaptureRequest(ex: ex);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqFailureMessage];
        Assert.Equal("detailed failure message", attr.StringValue);
        Assert.Equal(DeadLetterQueueServiceConstants.StringDataType, attr.DataType);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_TruncatesExceptionMessageWhenExceedsMaxLength()
    {
        var maxLength = DeadLetterQueueServiceConstants.Limits.MaxSqsMessageAttributeLength;
        var ex = new Exception(new string('x', maxLength + 50));
        var captured = await CallAndCaptureRequest(ex: ex);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqFailureMessage];
        Assert.Equal(maxLength, attr.StringValue.Length);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_DoesNotTruncateExceptionMessageWithinMaxLength()
    {
        var maxLength = DeadLetterQueueServiceConstants.Limits.MaxSqsMessageAttributeLength;
        var message = new string('x', maxLength - 1);
        var ex = new Exception(message);
        var captured = await CallAndCaptureRequest(ex: ex);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqFailureMessage];
        Assert.Equal(maxLength - 1, attr.StringValue.Length);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_SetsOriginalMessageIdAttribute()
    {
        var message = CreateMessage(messageId: "original-msg-id");
        var captured = await CallAndCaptureRequest(message: message);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqOriginalMessageId];
        Assert.Equal("original-msg-id", attr.StringValue);
        Assert.Equal(DeadLetterQueueServiceConstants.StringDataType, attr.DataType);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_SetsFailureTimestampAsUtcRoundtrip()
    {
        var before = DateTime.UtcNow;
        var captured = await CallAndCaptureRequest();
        var after = DateTime.UtcNow;

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqFailureTimestamp];
        var parsed = DateTime.Parse(attr.StringValue, null, System.Globalization.DateTimeStyles.RoundtripKind);

        Assert.Equal(DateTimeKind.Utc, parsed.Kind);
        Assert.InRange(parsed, before, after);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_SetsReceiveCountFromApproximateReceiveCountAttribute()
    {
        var message = CreateMessage(sqsAttributes: new Dictionary<string, string>
        {
            [DeadLetterQueueServiceConstants.SqsAttributes.ApproximateReceiveCount] = "7"
        });
        var captured = await CallAndCaptureRequest(message: message);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqReceiveCount];
        Assert.Equal("7", attr.StringValue);
        Assert.Equal(DeadLetterQueueServiceConstants.NumberDataType, attr.DataType);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_DefaultsReceiveCountToZeroWhenSqsAttributeAbsent()
    {
        var message = CreateMessage(sqsAttributes: []);
        var captured = await CallAndCaptureRequest(message: message);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqReceiveCount];
        Assert.Equal("0", attr.StringValue);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_DefaultsReceiveCountToZeroWhenSqsAttributesNull()
    {
        var message = CreateMessage(sqsAttributes: null);
        var captured = await CallAndCaptureRequest(message: message);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.DlqReceiveCount];
        Assert.Equal("0", attr.StringValue);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_AddsCorrelationIdFromContextWhenNotInOriginalAttributes()
    {
        CorrelationIdContext.Value = "injected-correlation-id";
        var message = CreateMessage(messageAttributes: []);
        var captured = await CallAndCaptureRequest(message: message);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.CorrelationId];
        Assert.Equal("injected-correlation-id", attr.StringValue);
        Assert.Equal(DeadLetterQueueServiceConstants.StringDataType, attr.DataType);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_PreservesExistingCorrelationIdFromOriginalAttributes()
    {
        CorrelationIdContext.Value = "new-context-id";
        var message = CreateMessage(messageAttributes: new Dictionary<string, MessageAttributeValue>
        {
            [DeadLetterQueueServiceConstants.MessageAttributes.CorrelationId] = new()
            {
                StringValue = "original-correlation-id",
                DataType = DeadLetterQueueServiceConstants.StringDataType
            }
        });
        var captured = await CallAndCaptureRequest(message: message);

        var attr = captured.MessageAttributes[DeadLetterQueueServiceConstants.MessageAttributes.CorrelationId];
        Assert.Equal("original-correlation-id", attr.StringValue);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_CopiesExistingMessageAttributesToDlqMessage()
    {
        var message = CreateMessage(messageAttributes: new Dictionary<string, MessageAttributeValue>
        {
            ["CustomHeader"] = new() { StringValue = "custom-value", DataType = "String" }
        });
        var captured = await CallAndCaptureRequest(message: message);

        Assert.True(captured.MessageAttributes.ContainsKey("CustomHeader"));
        Assert.Equal("custom-value", captured.MessageAttributes["CustomHeader"].StringValue);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_HandlesNullMessageAttributesWithoutThrowing()
    {
        var message = CreateMessage(messageAttributes: null);

        var result = await _sut.MoveToDeadLetterQueueAsync(
            message, QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        Assert.True(result);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenSendFails_ReturnsFalse()
    {
        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("SQS unavailable"));

        var result = await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenSendFails_DoesNotAttemptDelete()
    {
        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("SQS unavailable"));

        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        _sqs.Verify(
            x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenSendFails_LogsError()
    {
        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("SQS unavailable"));

        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        VerifyLogLevel(LogLevel.Error, Times.AtLeastOnce());
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenDeleteFails_ReturnsFalse()
    {
        _sqs
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Delete failed"));

        var result = await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenDeleteFails_StillAttemptedSend()
    {
        _sqs
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Delete failed"));

        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        _sqs.Verify(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenDeleteFails_LogsError()
    {
        _sqs
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Delete failed"));

        await _sut.MoveToDeadLetterQueueAsync(
            CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None);

        VerifyLogLevel(LogLevel.Error, Times.AtLeastOnce());
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenOperationCancelledDuringSend_Rethrows()
    {
        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _sut.MoveToDeadLetterQueueAsync(
                CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None));
    }

    [Fact]
    public async Task MoveToDeadLetterQueueAsync_WhenOperationCancelledDuringDelete_Rethrows()
    {
        _sqs
            .Setup(x => x.DeleteMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _sut.MoveToDeadLetterQueueAsync(
                CreateMessage(), QueueUrl, DlqUrl, new Exception(), CancellationToken.None));
    }

    private static Message CreateMessage(
        string messageId = "msg-001",
        string body = "{\"data\":\"test\"}",
        string receiptHandle = "receipt-handle-001",
        Dictionary<string, MessageAttributeValue>? messageAttributes = null,
        Dictionary<string, string>? sqsAttributes = null) => new()
        {
            MessageId = messageId,
            Body = body,
            ReceiptHandle = receiptHandle,
            MessageAttributes = messageAttributes,
            Attributes = sqsAttributes
        };

    private async Task<SendMessageRequest> CallAndCaptureRequest(
        Message? message = null,
        Exception? ex = null)
    {
        SendMessageRequest? captured = null;

        _sqs
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .Callback<SendMessageRequest, CancellationToken>((req, _) => captured = req)
            .ReturnsAsync(new SendMessageResponse { MessageId = "dlq-id" });

        await _sut.MoveToDeadLetterQueueAsync(
            message ?? CreateMessage(),
            QueueUrl,
            DlqUrl,
            ex ?? new Exception("test error"),
            CancellationToken.None);

        return captured!;
    }

    private void VerifyLogLevel(LogLevel level, Times times) =>
        _logger.Verify(
            x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
}