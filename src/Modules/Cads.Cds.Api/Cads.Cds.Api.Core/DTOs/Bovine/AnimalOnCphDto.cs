using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class AnimalOnCphDto
{
    [JsonPropertyName("identifier")]
    public AnimalIdentifierDto? Identifier { get; set; }

    [JsonPropertyName("birthDate")]
    public DateOnly? BirthDate { get; set; }

    [JsonPropertyName("movedOnCPH")]
    public DateOnly? MovedOnCph { get; set; }

    [JsonPropertyName("registeredOnCPH")]
    public DateOnly? RegisteredOnCph { get; set; }

    [JsonPropertyName("dateOffCPH")]
    public DateOnly? DateOffCph { get; set; }

    [JsonPropertyName("species")]
    public string? Species { get; set; }

    [JsonPropertyName("sex")]
    public string? Sex { get; set; }

    [JsonPropertyName("breedCode")]
    public BreedCodeDto? BreedCode { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}