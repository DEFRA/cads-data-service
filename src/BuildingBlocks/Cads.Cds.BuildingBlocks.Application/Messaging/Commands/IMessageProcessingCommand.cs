using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.BuildingBlocks.Application.Messaging.Commands;

public interface IMessageProcessingCommand : ICommand<MessageType>
{
}