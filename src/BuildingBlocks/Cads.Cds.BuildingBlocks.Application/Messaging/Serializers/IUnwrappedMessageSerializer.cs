using Cads.Cds.BuildingBlocks.Application.Messaging.Models;

namespace Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;

public interface IUnwrappedMessageSerializer<out T>
{
    T? Deserialize(UnwrappedMessage message);
}