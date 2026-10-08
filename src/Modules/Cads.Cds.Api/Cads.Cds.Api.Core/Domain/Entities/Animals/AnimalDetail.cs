namespace Cads.Cds.Api.Core.Domain.Entities.Animals;

public class AnimalDetail
{
    // Envelope
    public string? ResourceType { get; init; }
    public required string Identifier { get; init; }
    public string? EventDatetime { get; init; }
    public string? SourceSystem { get; init; }
    public string? SourceSchema { get; init; }
    public string? SourceSchemaVersion { get; init; }

    // Animal
    public string? AnimalResourceType { get; init; }
    public string? IdentifierSchema { get; init; }
    public string? Species { get; init; }
    public string? Sex { get; init; }
    public DateOnly? BirthDate { get; init; }
    public DateOnly? RegistrationDate { get; init; }
    public DateOnly? DateOnCph { get; init; }

    // Breed
    public string? BreedSchema { get; init; }
    public string? BreedCode { get; init; }
    public string? BreedDisplayNameLong { get; init; }
    public string? BreedDisplayName { get; init; }
    public string? BreedName { get; init; }

    // Status
    public string? State { get; init; }
    public string? RestrictionStatus { get; init; }

    // Parentage
    public string? GeneticDamIdentifier { get; init; }
    public string? GeneticDamSchema { get; init; }
    public string? SireIdentifier { get; init; }
    public string? SireSchema { get; init; }
}