using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Application.DTOs.Holdings;

public class HoldingIdentifierDto
{
    [JsonPropertyName("schema")]
    public string Schema { get; init; } = string.Empty;

    [JsonPropertyName("identifier")]
    public string Identifier { get; init; } = string.Empty;
}