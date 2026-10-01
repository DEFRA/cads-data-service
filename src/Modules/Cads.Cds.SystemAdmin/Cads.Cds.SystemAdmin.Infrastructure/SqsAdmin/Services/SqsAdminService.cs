using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.SqsAdmin.Services;
using Cads.Cds.SystemAdmin.Core.Configuration;
using Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.SystemAdmin.Infrastructure.SqsAdmin.Services;

public class SqsAdminService(
    IAmazonSQS sqs,
    IOptionsMonitor<Dictionary<string, SqsAdminQueueOptions>> options,
    ILogger<SqsAdminService> logger) : ISqsAdminService
{
    private const int MaxMessagesPerSqsRequest = 10;

    private static readonly string[] MetricAttributeNames =
    [
        "ApproximateNumberOfMessages",
        "ApproximateNumberOfMessagesNotVisible",
        "ApproximateNumberOfMessagesDelayed"
    ];

    public Task<IReadOnlyList<QueueInfoDto>> GetQueuesAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<QueueInfoDto> queues = GetConfiguredQueues()
            .Select(q => new QueueInfoDto(q.Key, q.Value.QueueUrl, q.Value.DlqQueueUrl))
            .OrderBy(q => q.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        return Task.FromResult(queues);
    }

    public async Task<QueueMetricsDto> GetMetricsAsync(string queue, CancellationToken cancellationToken = default)
    {
        var queueUrl = ResolveQueueUrl(queue);

        var attributesResponse = await sqs.GetQueueAttributesAsync(
            new GetQueueAttributesRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = [.. MetricAttributeNames]
            },
            cancellationToken);

        var oldestMessageAgeSeconds = await GetOldestMessageAgeSecondsAsync(queueUrl, cancellationToken);

        return new QueueMetricsDto(
            GetLongAttribute(attributesResponse.Attributes, "ApproximateNumberOfMessages"),
            GetLongAttribute(attributesResponse.Attributes, "ApproximateNumberOfMessagesNotVisible"),
            GetLongAttribute(attributesResponse.Attributes, "ApproximateNumberOfMessagesDelayed"),
            oldestMessageAgeSeconds);
    }

    public async Task<IReadOnlyList<QueueMessageDto>> PeekMessagesAsync(
        PeekMessagesRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var queueUrl = ResolveQueueUrl(request.Queue);

        var response = await sqs.ReceiveMessageAsync(
            new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = Math.Clamp(request.MaxMessages, 1, MaxMessagesPerSqsRequest),
                // VisibilityTimeout is always 0: this is a peek-only operation, so messages must
                // never be hidden from other consumers, even momentarily.
                VisibilityTimeout = 0,
                MessageAttributeNames = ["All"],
                MessageSystemAttributeNames = ["All"]
            },
            cancellationToken);

        // Non-destructive peek: messages are never deleted and never hidden (VisibilityTimeout=0),
        // satisfying the "peek" contract.
        return response.Messages
            .Select(MapToQueueMessageDto)
            .ToList();
    }

    public async Task<ReplayResultDto> ReplayDlqAsync(ReplayDlqRequestDto request, CancellationToken cancellationToken = default)
    {
        var queueOptions = ResolveQueueOptions(request.Queue);

        if (string.IsNullOrWhiteSpace(queueOptions.DlqQueueUrl))
        {
            throw new UnprocessableException($"Queue '{request.Queue}' does not have a configured DLQ.");
        }

        var dlqUrl = queueOptions.DlqQueueUrl;
        var mainQueueUrl = queueOptions.QueueUrl;
        var batchSize = request.BatchSize > 0 ? request.BatchSize : queueOptions.DefaultReplayBatchSize;

        var moved = 0;
        var errors = new List<string>();

        while (moved + errors.Count < batchSize)
        {
            var remaining = batchSize - (moved + errors.Count);

            var receiveResponse = await sqs.ReceiveMessageAsync(
                new ReceiveMessageRequest
                {
                    QueueUrl = dlqUrl,
                    MaxNumberOfMessages = Math.Clamp(remaining, 1, MaxMessagesPerSqsRequest),
                    MessageAttributeNames = ["All"],
                    MessageSystemAttributeNames = ["All"]
                },
                cancellationToken);

            if (receiveResponse.Messages.Count == 0)
            {
                break;
            }

            foreach (var message in receiveResponse.Messages)
            {
                try
                {
                    await sqs.SendMessageAsync(
                        new SendMessageRequest
                        {
                            QueueUrl = mainQueueUrl,
                            MessageBody = message.Body,
                            MessageAttributes = message.MessageAttributes
                        },
                        cancellationToken);

                    await sqs.DeleteMessageAsync(dlqUrl, message.ReceiptHandle, cancellationToken);

                    moved++;

                    if (logger.IsEnabled(LogLevel.Information))
                    {
                        logger.LogInformation(
                            "DLQ replay: moved MessageId={MessageId} from {DlqUrl} to {MainQueueUrl}",
                            message.MessageId, dlqUrl, mainQueueUrl);
                    }
                }
                catch (Exception ex)
                {
                    // Graceful partial replay: leave the message on the DLQ (its visibility
                    // timeout will expire, allowing a later retry) and keep processing the batch.
                    errors.Add($"MessageId '{message.MessageId}': {ex.Message}");

                    logger.LogError(
                        ex,
                        "DLQ replay failed for MessageId={MessageId} on {DlqUrl}",
                        message.MessageId, dlqUrl);
                }
            }
        }

        return new ReplayResultDto(moved, errors.Count, errors);
    }

    private async Task<long> GetOldestMessageAgeSecondsAsync(string queueUrl, CancellationToken cancellationToken)
    {
        // SQS has no direct "oldest message age" attribute via GetQueueAttributes, so this is
        // approximated by peeking the head of the queue and inspecting its SentTimestamp.
        // VisibilityTimeout=0 ensures the peek does not consume/hide the message.
        var response = await sqs.ReceiveMessageAsync(
            new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = 1,
                VisibilityTimeout = 0,
                MessageSystemAttributeNames = ["SentTimestamp"]
            },
            cancellationToken);

        var message = response.Messages.FirstOrDefault();
        if (message == null) return 0;

        if (!message.Attributes.TryGetValue("SentTimestamp", out var sentTimestampRaw) ||
            !long.TryParse(sentTimestampRaw, out var sentTimestampMs))
        {
            return 0;
        }

        var sentAt = DateTimeOffset.FromUnixTimeMilliseconds(sentTimestampMs);
        var age = DateTimeOffset.UtcNow - sentAt;

        return age.Ticks > 0 ? (long)age.TotalSeconds : 0;
    }

    private static QueueMessageDto MapToQueueMessageDto(Message message)
    {
        DateTimeOffset? sentTimestamp = null;
        if (message.Attributes.TryGetValue("SentTimestamp", out var sentTimestampRaw) &&
            long.TryParse(sentTimestampRaw, out var sentTimestampMs))
        {
            sentTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(sentTimestampMs);
        }

        var attributes = message.Attributes
            .ToDictionary(a => a.Key, a => a.Value);

        return new QueueMessageDto(message.MessageId, message.Body, attributes, sentTimestamp);
    }

    private static long GetLongAttribute(IDictionary<string, string> attributes, string key) =>
        attributes.TryGetValue(key, out var value) && long.TryParse(value, out var parsed) ? parsed : 0;

    private string ResolveQueueUrl(string queue) => ResolveQueueOptions(queue).QueueUrl;

    private SqsAdminQueueOptions ResolveQueueOptions(string queue)
    {
        var queues = GetConfiguredQueues();

        if (!queues.TryGetValue(queue, out var queueOptions))
        {
            throw new NotFoundException("Queue", queue);
        }

        return queueOptions;
    }

    private Dictionary<string, SqsAdminQueueOptions> GetConfiguredQueues() =>
        options.CurrentValue;
}