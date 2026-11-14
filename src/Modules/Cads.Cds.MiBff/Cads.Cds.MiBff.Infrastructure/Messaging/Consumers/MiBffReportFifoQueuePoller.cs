using Amazon.SQS;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Consumers;

public class MiBffReportFifoQueuePoller(
    IServiceScopeFactory scopeFactory,
    IAmazonSQS sqs,
    MessageCommandRegistry<MiBffFifoQueueClient> registry,
    IOptionsMonitor<QueueConsumerOptions> options,
    MiBffFifoQueueClient client,
    IQueueAdminService<MiBffFifoQueueClient> queueAdminService,
    IQueuePollerObserver<MessageType> observer,
    ILogger<MiBffReportFifoQueuePoller> logger)
    : MediatorSqsQueuePoller<MiBffFifoQueueClient>(scopeFactory, sqs, registry, options, client, queueAdminService, observer, logger);