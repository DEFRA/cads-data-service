using System.Text.Json.Serialization;

namespace Cads.Cds.Api.Core.DTOs.Bovine;

public class AnimalDto
{
    [JsonPropertyName("resourceType")]
    public string? ResourceType { get; set; }

    [JsonPropertyName("identifier")]
    public AnimalIdentifierDto? Identifier { get; set; }

    [JsonPropertyName("species")]
    public string? Species { get; set; }

    [JsonPropertyName("sex")]
    public string? Sex { get; set; }

    [JsonPropertyName("birthDate")]
    public DateOnly? BirthDate { get; set; }

    [JsonPropertyName("registrationDate")]
    public DateOnly? RegistrationDate { get; set; }

    [JsonPropertyName("dateOnCph")]
    public DateOnly? DateOnCph { get; set; }

    [JsonPropertyName("breedCode")]
    public BreedCodeDto? BreedCode { get; set; }

    [JsonPropertyName("parentage")]
    public IEnumerable<ParentageDto>? Parentage { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("restrictionStatus")]
    public string? RestrictionStatus { get; set; }
}