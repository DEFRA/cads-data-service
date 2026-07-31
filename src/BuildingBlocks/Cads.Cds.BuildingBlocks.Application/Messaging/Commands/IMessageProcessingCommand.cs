using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Messages;

namespace Cads.Cds.BuildingBlocks.Application.Messaging.Commands;

public interface IMessageProcessingCommand : ICommand<MessageType>
{
}
