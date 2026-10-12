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

    // Current location
    public string? CurrentIdentifier { get; init; }
    public string? CurrentSubIdentifier { get; init; }
    public string? CurrentFullIdentifier { get; init; }
    public string? CurrentName { get; init; }
    public string? CurrentPremisesType { get; init; }

    // Registered location
    public string? RegisteredIdentifier { get; init; }
    public string? RegisteredSubIdentifier { get; init; }
    public string? RegisteredFullIdentifier { get; init; }
    public string? RegisteredName { get; init; }
    public string? RegisteredPremisesType { get; init; }
    
    // Death location
    public string? DeathIdentifier { get; init; }
    public string? DeathSubIdentifier { get; init; }
    public string? DeathFullIdentifier { get; init; }
    public string? DeathName { get; init; }
    public string? DeathPremisesType { get; init; }

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