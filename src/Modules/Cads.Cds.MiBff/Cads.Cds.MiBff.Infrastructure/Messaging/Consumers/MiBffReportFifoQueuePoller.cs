using Amazon.SQS;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Consumers;

public class MiBffReportFifoQueuePoller(
    IServiceScopeFactory scopeFactory,
    IAmazonSQS sqs,
    IOptionsMonitor<QueueConsumerOptions> options,
    MiBffFifoQueueClient client,
    IQueueAdminService<MiBffFifoQueueClient> queueAdminService,
    IQueuePollerObserver<MessageType> observer,
    ILogger<MiBffReportFifoQueuePoller> logger) 
    : BaseSqsQueuePoller<MiBffFifoQueueClient>(scopeFactory, sqs, options, client, queueAdminService, observer, logger)
{
    protected override Task<MessageType?> ProcessMessageAsync(UnwrappedMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}