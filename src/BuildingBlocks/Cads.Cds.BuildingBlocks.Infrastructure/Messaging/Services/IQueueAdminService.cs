using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;

public interface IQueueAdminService<in T>
where T : IQueueClient
{
    Task<bool> MoveToDeadLetterQueueAsync(
        Message message,
        string queueUrl,
        string? dlqQueueUrl,
        Exception ex,
        CancellationToken cancellationToken);
}