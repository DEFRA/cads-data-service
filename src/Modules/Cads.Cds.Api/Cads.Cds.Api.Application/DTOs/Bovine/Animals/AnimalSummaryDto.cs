using Cads.Cds.Api.Application.DTOs.Animals;
using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Application.DTOs.Bovine.Animals;

public class AnimalSummaryDto
{
    [JsonPropertyName("identifier")]
    public AnimalIdentifierDto? Identifier { get; init; }

    [JsonPropertyName("birthDate")]
    public DateOnly? BirthDate { get; init; }

    [JsonPropertyName("dateOnCPH")]
    public DateOnly? DateOnCph { get; init; }

    [JsonPropertyName("dateOffCPH")]
    public DateOnly? DateOffCph { get; init; }

    [JsonPropertyName("species")]
    public string? Species { get; init; }

    [JsonPropertyName("sex")]
    public string? Sex { get; init; }

    [JsonPropertyName("breedCode")]
    public BreedCodeDto? BreedCode { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}