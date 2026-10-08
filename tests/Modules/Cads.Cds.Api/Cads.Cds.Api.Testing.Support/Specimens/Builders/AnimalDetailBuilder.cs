using AutoFixture.Kernel;
using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Cads.Cds.Api.Testing.Support.Constants;

namespace Cads.Cds.Api.Testing.Support.Specimens.Builders;

public record AnimalDetailSpec(string Identifier, bool HasParents, bool IsDead);

public class AnimalDetailBuilder(IEnumerable<AnimalDetailSpec> specs) : ISpecimenBuilder
{
    private const string EarTagSchema = "uk.gov.defra.ear-tag.conventional";

    private readonly Queue<AnimalDetailSpec> _specs = new(specs);

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(AnimalDetail) || _specs.Count == 0)
            return new NoSpecimen();

        var spec = _specs.Dequeue();

        return new AnimalDetail
        {
            ResourceType = "AnimalDetail",
            Identifier = spec.Identifier,
            EventDatetime = "2026-08-24T12:00:00Z",
            SourceSystem = "CTS",
            SourceSchema = "animal_details",
            SourceSchemaVersion = "1.0",

            AnimalResourceType = "Animal",
            IdentifierSchema = EarTagSchema,
            Species = "Cattle",
            Sex = "Female",
            BirthDate = new DateOnly(2023, 03, 10),
            RegistrationDate = new DateOnly(2023, 03, 14),
            DateOnCph = new DateOnly(2023, 03, 10),

            BreedSchema = "cts.breed",
            BreedCode = "HO",
            BreedDisplayNameLong = "Holstein Friesian",
            BreedDisplayName = "Holstein Friesian",
            BreedName = "Holstein Friesian",

            State = spec.IsDead ? "Dead" : "Alive",
            RestrictionStatus = spec.IsDead ? "Unrestricted" : "Restricted",

            GeneticDamIdentifier = spec.HasParents ? TestBovineConstants.KnownDamIdentifier : null,
            GeneticDamSchema = spec.HasParents ? EarTagSchema : null,
            SireIdentifier = spec.HasParents ? TestBovineConstants.KnownSireIdentifier : null,
            SireSchema = spec.HasParents ? EarTagSchema : null
        };
    }
}