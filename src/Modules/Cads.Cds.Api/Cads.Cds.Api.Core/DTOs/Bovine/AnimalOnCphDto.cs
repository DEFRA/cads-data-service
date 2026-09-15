using Cads.Cds.Api.Core.Domain.Bovine;
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
    public AnimalSpecies? Species { get; set; }

    [JsonPropertyName("sex")]
    public AnimalSex? Sex { get; set; }

    [JsonPropertyName("breedCode")]
    public BreedCodeDto? BreedCode { get; set; }

    [JsonPropertyName("status")]
    public AnimalStatus? Status { get; set; }
}