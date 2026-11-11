using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.StorageBridge.Application.Messaging.Commands;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Factories;

public sealed class S3ToPostgresCopyMessageCommandFactory : IMessageCommandFactory
{
    public IMessageProcessingCommand Create(UnwrappedMessage message)
        => new ProcessS3ToPostgresCopyMessageCommand(message);
}
