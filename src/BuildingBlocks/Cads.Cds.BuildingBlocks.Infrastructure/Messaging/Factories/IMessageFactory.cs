using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;

public interface IMessageFactory
{
    SendMessageRequest CreateFifoSqsMessage<TBody>(
        string queueUrl,
        TBody body,
        FifoMessageMetadata metadata,
        string? subject = null);
}