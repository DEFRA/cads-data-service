using Cads.Cds.StorageBridge.Application.Messaging.Messages;
using System.Text.Json.Serialization;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Serializers;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = []
)]
[JsonSerializable(typeof(S3ToPostgresCopyMessage))]
public partial class StorageBridgeSerializerContext : JsonSerializerContext
{
}
