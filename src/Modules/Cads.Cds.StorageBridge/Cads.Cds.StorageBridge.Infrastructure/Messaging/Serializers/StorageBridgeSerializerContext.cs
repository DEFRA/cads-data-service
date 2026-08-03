using Cads.Cds.ApiSurface.Messages.Imports;
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