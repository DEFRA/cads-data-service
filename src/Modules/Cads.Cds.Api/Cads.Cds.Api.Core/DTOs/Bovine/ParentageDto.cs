using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class ParentageDto
{
    [JsonPropertyName("relationship")]
    public string? Relationship { get; set; }

    [JsonPropertyName("animalIdentifier")]
    public AnimalIdentifierDto? AnimalIdentifier { get; set; }
}