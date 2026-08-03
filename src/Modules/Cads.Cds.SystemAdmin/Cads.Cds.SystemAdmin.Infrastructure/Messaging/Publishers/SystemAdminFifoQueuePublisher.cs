using Amazon.SQS;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.SystemAdmin.Application.Messaging.Clients;
using Microsoft.Extensions.Options;
using System.Net;

namespace Cads.Cds.SystemAdmin.Infrastructure.Messaging.Publishers;

public class SystemAdminFifoQueuePublisher(
    IAmazonSQS sqs,
    IMessageFactory messageFactory,
    IOptionsMonitor<QueuePublisherOptions> options,
    SystemAdminFifoQueueClient client)
    : IMessagePublisher<SystemAdminFifoQueueClient>
{
    private readonly IAmazonSQS _sqs = sqs;
    private readonly IMessageFactory _messageFactory = messageFactory;
    private readonly IOptionsMonitor<QueuePublisherOptions> _options = options;
    private readonly SystemAdminFifoQueueClient _client = client;

    public string QueueUrl => _options.Get(_client.ClientName).QueueUrl;

    public async Task PublishAsync<TMessage>(TMessage? message, FifoMessageMetadata metadata, CancellationToken cancellationToken = default)
        where TMessage : class
    {
        if (message == null) throw new ArgumentException("Message payload was null", nameof(message));

        if (string.IsNullOrWhiteSpace(QueueUrl)) throw new PublishFailedException("QueueUrl is missing", false);

        try
        {
            var sendRequest = _messageFactory.CreateFifoSqsMessage(QueueUrl, message, metadata);

            await _sqs.SendMessageAsync(sendRequest, cancellationToken);
        }
        catch (Exception ex)
        {
            var isTransient = ex is AmazonSQSException sqsEx &&
                              sqsEx.StatusCode is >= HttpStatusCode.InternalServerError
                                  or HttpStatusCode.TooManyRequests;

            throw new PublishFailedException($"Failed to publish message on {QueueUrl}.", isTransient, ex);
        }
    }
}