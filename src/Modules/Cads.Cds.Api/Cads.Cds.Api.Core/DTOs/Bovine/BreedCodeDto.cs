using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class BreedCodeDto
{
    [JsonPropertyName("schema")]
    public string? Schema { get; set; }

    [JsonPropertyName("breedName")]
    public string? BreedName { get; set; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }
}