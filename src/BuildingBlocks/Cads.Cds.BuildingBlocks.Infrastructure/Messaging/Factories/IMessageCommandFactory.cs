using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;

public interface IMessageCommandFactory
{
    IMessageProcessingCommand Create(UnwrappedMessage message);
}