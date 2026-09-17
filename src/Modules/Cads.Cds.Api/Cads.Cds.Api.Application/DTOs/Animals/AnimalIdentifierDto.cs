using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Application.DTOs.Animals;

public class AnimalIdentifierDto
{
    [JsonPropertyName("schema")]
    public required string Schema { get; set; }

    [JsonPropertyName("identifier")]
    public required string Identifier { get; set; }
}