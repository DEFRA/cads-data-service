using Cads.Cds.ApiSurface.Messages.Reports;
using System.Text.Json.Serialization;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Serializers;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = []
)]
[JsonSerializable(typeof(ReportJobRequestedMessage))]
public partial class MiBffSerializerContext : JsonSerializerContext
{
}