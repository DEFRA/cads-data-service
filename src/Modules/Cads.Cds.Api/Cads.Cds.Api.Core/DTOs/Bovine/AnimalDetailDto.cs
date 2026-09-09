using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class AnimalDetailDto
{
    [JsonPropertyName("resourceType")]
    public string? ResourceType { get; set; }

    [JsonPropertyName("identifier")]
    public required string Identifier { get; set; }

    [JsonPropertyName("eventDateTime")]
    public DateTime? EventDateTime { get; set; }

    [JsonPropertyName("source")]
    public AnimalDetailSourceDto? Source { get; set; }

    [JsonPropertyName("animalDetail")]
    public AnimalDto? AnimalDetail { get; set; }
}