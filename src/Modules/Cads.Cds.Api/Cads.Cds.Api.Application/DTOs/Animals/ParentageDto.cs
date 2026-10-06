using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Application.DTOs.Animals;

public class ParentageDto
{
    [JsonPropertyName("relationship")]
    public string? Relationship { get; set; }

    [JsonPropertyName("animalIdentifier")]
    public AnimalIdentifierDto? AnimalIdentifier { get; set; }
}