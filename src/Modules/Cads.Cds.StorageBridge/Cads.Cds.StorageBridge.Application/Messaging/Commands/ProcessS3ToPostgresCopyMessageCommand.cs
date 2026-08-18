using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;

namespace Cads.Cds.StorageBridge.Application.Messaging.Commands;

public sealed record ProcessS3ToPostgresCopyMessageCommand(UnwrappedMessage Message)
    : IMessageProcessingCommand;