
using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class AnimalIdentifierDto
{
    [JsonPropertyName("schema")]
    public string? Schema { get; set; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }
}