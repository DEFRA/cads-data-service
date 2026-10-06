namespace Cads.Cds.Api.Core.Domain.Entities.Animals;

public class AnimalOnHolding
{
    public required string CphNumber { get; init; }
    public required decimal AnimalId { get; init; }
    public string? EarTagNumber { get; init; }
    public string? EarTagUrlIdentifier { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public DateOnly? DateRegistered { get; init; }
    public string? Sex { get; init; }
    public string? BreedCode { get; init; }
    public string? Breed { get; init; }
    public string? AnimalStatus { get; init; }
    public string? ResourceType { get; init; }
    public string? CphSchema { get; init; }
    public string? LocationName { get; init; }
    public string? IdentifierSchema { get; init; }
    public DateOnly? DateOnCph { get; init; }
    public DateOnly? DateOffCph { get; init; }
    public string? Species { get; init; }
    public string? BreedSchema { get; init; }
    public string? BreedName { get; init; }
    public string? BreedIdentifier { get; init; }
    public long TotalCount { get; init; }
}