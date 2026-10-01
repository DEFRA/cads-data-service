using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Core.Configuration;
using Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;
using Cads.Cds.SystemAdmin.Infrastructure.SqsAdmin.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.SqsAdmin.Services;

public class SqsAdminServiceTests
{
    private const string QueueName = "CadsCds";
    private const string QueueUrl = "https://sqs.test/000000000000/cads-cds";
    private const string DlqUrl = "https://sqs.test/000000000000/cads-cds-dlq";

    private readonly Mock<IAmazonSQS> _sqsMock = new();
    private readonly Mock<IOptionsMonitor<Dictionary<string, SqsAdminQueueOptions>>> _optionsMock = new();
    private readonly Mock<ILogger<SqsAdminService>> _loggerMock = new();

    private Dictionary<string, SqsAdminQueueOptions> _queues = new()
    {
        [QueueName] = new SqsAdminQueueOptions { QueueUrl = QueueUrl, DlqQueueUrl = DlqUrl, DefaultReplayBatchSize = 10 }
    };

    private SqsAdminService CreateSut()
    {
        _optionsMock.Setup(x => x.CurrentValue).Returns(_queues);
        return new SqsAdminService(_sqsMock.Object, _optionsMock.Object, _loggerMock.Object);
    }

    // GetQueuesAsync

    [Fact]
    public async Task GetQueuesAsync_ShouldReturnConfiguredQueues_OrderedByName()
    {
        _queues = new Dictionary<string, SqsAdminQueueOptions>
        {
            ["ZQueue"] = new() { QueueUrl = "url-z" },
            ["AQueue"] = new() { QueueUrl = "url-a", DlqQueueUrl = "dlq-a" }
        };

        var sut = CreateSut();

        var result = await sut.GetQueuesAsync(CancellationToken.None);

        result.Should().HaveCount(2);
        result[0].Should().Be(new QueueInfoDto("AQueue", "url-a", "dlq-a"));
        result[1].Should().Be(new QueueInfoDto("ZQueue", "url-z", null));
    }

    // GetMetricsAsync

    [Fact]
    public async Task GetMetricsAsync_ShouldReturnMetrics_WhenQueueExists()
    {
        var sut = CreateSut();

        _sqsMock
            .Setup(x => x.GetQueueAttributesAsync(
                It.Is<GetQueueAttributesRequest>(r => r.QueueUrl == QueueUrl),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetQueueAttributesResponse
            {
                Attributes = new Dictionary<string, string>
                {
                    ["ApproximateNumberOfMessages"] = "5",
                    ["ApproximateNumberOfMessagesNotVisible"] = "2",
                    ["ApproximateNumberOfMessagesDelayed"] = "1"
                }
            });

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(
                It.Is<ReceiveMessageRequest>(r => r.QueueUrl == QueueUrl),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { Messages = [] });

        var result = await sut.GetMetricsAsync(QueueName, CancellationToken.None);

        result.ApproximateNumberOfMessages.Should().Be(5);
        result.ApproximateNumberOfMessagesNotVisible.Should().Be(2);
        result.ApproximateNumberOfMessagesDelayed.Should().Be(1);
        result.OldestMessageAgeSeconds.Should().Be(0);
    }

    [Fact]
    public async Task GetMetricsAsync_ShouldComputeOldestMessageAge_WhenHeadMessagePresent()
    {
        var sut = CreateSut();

        _sqsMock
            .Setup(x => x.GetQueueAttributesAsync(It.IsAny<GetQueueAttributesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetQueueAttributesResponse { Attributes = new Dictionary<string, string>() });

        var sentAt = DateTimeOffset.UtcNow.AddSeconds(-30);

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse
            {
                Messages =
                [
                    new Message
                    {
                        Attributes = new Dictionary<string, string>
                        {
                            ["SentTimestamp"] = sentAt.ToUnixTimeMilliseconds().ToString()
                        }
                    }
                ]
            });

        var result = await sut.GetMetricsAsync(QueueName, CancellationToken.None);

        result.OldestMessageAgeSeconds.Should().BeGreaterThanOrEqualTo(29);
    }

    [Fact]
    public async Task GetMetricsAsync_ShouldThrowNotFoundException_WhenQueueNotConfigured()
    {
        var sut = CreateSut();

        var action = () => sut.GetMetricsAsync("unknown-queue", CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    // PeekMessagesAsync

    [Fact]
    public async Task PeekMessagesAsync_ShouldReturnMappedMessages()
    {
        var sut = CreateSut();
        var sentAt = DateTimeOffset.UtcNow.AddMinutes(-1);

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(
                It.Is<ReceiveMessageRequest>(r => r.QueueUrl == QueueUrl && r.VisibilityTimeout == 0),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse
            {
                Messages =
                [
                    new Message
                    {
                        MessageId = "msg-1",
                        Body = "{\"foo\":\"bar\"}",
                        Attributes = new Dictionary<string, string>
                        {
                            ["SentTimestamp"] = sentAt.ToUnixTimeMilliseconds().ToString()
                        }
                    }
                ]
            });

        var result = await sut.PeekMessagesAsync(new PeekMessagesRequestDto(QueueName, 5), CancellationToken.None);

        result.Should().ContainSingle();
        result[0].MessageId.Should().Be("msg-1");
        result[0].Body.Should().Be("{\"foo\":\"bar\"}");
        result[0].SentTimestamp.Should().NotBeNull();
    }

    [Fact]
    public async Task PeekMessagesAsync_ShouldClampMaxMessages_ToSqsLimit()
    {
        var sut = CreateSut();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(
                It.Is<ReceiveMessageRequest>(r => r.MaxNumberOfMessages == 10),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { Messages = [] });

        await sut.PeekMessagesAsync(new PeekMessagesRequestDto(QueueName, 999), CancellationToken.None);

        _sqsMock.Verify(x => x.ReceiveMessageAsync(
            It.Is<ReceiveMessageRequest>(r => r.MaxNumberOfMessages == 10),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task PeekMessagesAsync_ShouldThrowNotFoundException_WhenQueueNotConfigured()
    {
        var sut = CreateSut();

        var action = () => sut.PeekMessagesAsync(new PeekMessagesRequestDto("unknown-queue", 5), CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    // ReplayDlqAsync

    [Fact]
    public async Task ReplayDlqAsync_ShouldThrowUnprocessableException_WhenNoDlqConfigured()
    {
        _queues = new Dictionary<string, SqsAdminQueueOptions>
        {
            [QueueName] = new() { QueueUrl = QueueUrl, DlqQueueUrl = null }
        };
        var sut = CreateSut();

        var action = () => sut.ReplayDlqAsync(new ReplayDlqRequestDto(QueueName, 5), CancellationToken.None);

        await action.Should().ThrowAsync<UnprocessableException>();
    }

    [Fact]
    public async Task ReplayDlqAsync_ShouldThrowNotFoundException_WhenQueueNotConfigured()
    {
        var sut = CreateSut();

        var action = () => sut.ReplayDlqAsync(new ReplayDlqRequestDto("unknown-queue", 5), CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ReplayDlqAsync_ShouldMoveAllMessages_WhenAllSucceed()
    {
        var sut = CreateSut();

        var messages = new List<Message>
        {
            new() { MessageId = "m1", Body = "b1", ReceiptHandle = "r1" },
            new() { MessageId = "m2", Body = "b2", ReceiptHandle = "r2" }
        };

        SetupSingleReceiveThenEmpty(messages);

        _sqsMock
            .Setup(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SendMessageResponse());

        _sqsMock
            .Setup(x => x.DeleteMessageAsync(DlqUrl, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteMessageResponse());

        var result = await sut.ReplayDlqAsync(new ReplayDlqRequestDto(QueueName, 2), CancellationToken.None);

        result.Moved.Should().Be(2);
        result.Failed.Should().Be(0);
        result.Errors.Should().BeEmpty();

        _sqsMock.Verify(x => x.SendMessageAsync(
            It.Is<SendMessageRequest>(r => r.QueueUrl == QueueUrl),
            It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ReplayDlqAsync_ShouldRecordPartialFailures_AndContinueProcessing()
    {
        var sut = CreateSut();

        var messages = new List<Message>
        {
            new() { MessageId = "m1", Body = "b1", ReceiptHandle = "r1" },
            new() { MessageId = "m2", Body = "b2", ReceiptHandle = "r2" }
        };

        SetupSingleReceiveThenEmpty(messages);

        _sqsMock
            .SetupSequence(x => x.SendMessageAsync(It.IsAny<SendMessageRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonSQSException("boom"))
            .ReturnsAsync(new SendMessageResponse());

        _sqsMock
            .Setup(x => x.DeleteMessageAsync(DlqUrl, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteMessageResponse());

        var result = await sut.ReplayDlqAsync(new ReplayDlqRequestDto(QueueName, 2), CancellationToken.None);

        result.Moved.Should().Be(1);
        result.Failed.Should().Be(1);
        result.Errors.Should().ContainSingle(e => e.Contains("m1"));
    }

    [Fact]
    public async Task ReplayDlqAsync_ShouldStopEarly_WhenDlqHasNoMoreMessages()
    {
        var sut = CreateSut();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(
                It.Is<ReceiveMessageRequest>(r => r.QueueUrl == DlqUrl),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { Messages = [] });

        var result = await sut.ReplayDlqAsync(new ReplayDlqRequestDto(QueueName, 5), CancellationToken.None);

        result.Moved.Should().Be(0);
        result.Failed.Should().Be(0);
    }

    [Fact]
    public async Task ReplayDlqAsync_ShouldUseConfiguredDefaultBatchSize_WhenRequestBatchSizeIsZero()
    {
        var sut = CreateSut();

        _sqsMock
            .Setup(x => x.ReceiveMessageAsync(
                It.Is<ReceiveMessageRequest>(r => r.QueueUrl == DlqUrl && r.MaxNumberOfMessages == 10),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { Messages = [] });

        await sut.ReplayDlqAsync(new ReplayDlqRequestDto(QueueName, 0), CancellationToken.None);

        _sqsMock.Verify(x => x.ReceiveMessageAsync(
            It.Is<ReceiveMessageRequest>(r => r.QueueUrl == DlqUrl && r.MaxNumberOfMessages == 10),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    private void SetupSingleReceiveThenEmpty(List<Message> messages)
    {
        _sqsMock
            .SetupSequence(x => x.ReceiveMessageAsync(
                It.Is<ReceiveMessageRequest>(r => r.QueueUrl == DlqUrl),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReceiveMessageResponse { Messages = messages })
            .ReturnsAsync(new ReceiveMessageResponse { Messages = [] });
    }
}