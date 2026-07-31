using Cads.Cds.BuildingBlocks.Application.Messaging.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Cads.Cds.BuildingBlocks.Application.Messaging.Serializers;

public class MessageIdentifierSerializer<T>(JsonTypeInfo<T> typeInfo) : IUnwrappedMessageSerializer<T>
where T : MessageType
{
    private readonly JsonTypeInfo<T> _typeInfo = typeInfo;

    public T? Deserialize(UnwrappedMessage message)
    {
        try
        {
            return JsonSerializer.Deserialize(message.Payload, _typeInfo);
        }
        catch
        {
            return null;
        }
    }
}