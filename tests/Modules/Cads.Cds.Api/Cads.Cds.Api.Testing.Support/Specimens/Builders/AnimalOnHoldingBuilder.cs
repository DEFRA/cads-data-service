using AutoFixture.Kernel;
using Cads.Cds.Api.Core.Domain.Entities.Animals;

namespace Cads.Cds.Api.Testing.Support.Specimens.Builders;

public class AnimalOnHoldingBuilder(string cph, string locationName, int idOffset) : ISpecimenBuilder
{
    private static readonly (string Code, string Name)[] s_breeds =
    [
        ("HO", "Holstein Friesian"),
        ("HL", "HIGHLAND"),
        ("WW", "WELSH WHITE")
    ];

    private int _index;

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(AnimalOnHolding))
            return new NoSpecimen();

        var i = _index++;
        var (breedCode, breedName) = s_breeds[i % s_breeds.Length];

        // Deterministic so tests can assert on filters, sort order and paging
        var birthDate = new DateOnly(2022, 01, 01).AddDays(i * 10);
        var earTag = $"UK3245371{idOffset + i:D5}";

        return new AnimalOnHolding
        {
            CphNumber = cph,
            AnimalId = idOffset + i + 1,                 // unique: part of the fake key
            EarTagNumber = earTag,
            EarTagUrlIdentifier = earTag,
            DateOfBirth = birthDate,
            DateRegistered = birthDate.AddDays(2),
            Sex = i % 2 == 0 ? "F" : "M",
            BreedCode = breedCode,
            Breed = breedName,
            AnimalStatus = "Alive",
            ResourceType = "AnimalCollection",
            CphSchema = "uk.gov.defra.cph",
            LocationName = locationName,
            IdentifierSchema = "uk.gov.defra.ear-tag.conventional",
            DateOnCph = birthDate.AddDays(7),
            DateOffCph = null,
            Species = "Cattle",
            BreedSchema = "cts.breed",
            BreedName = breedName,
            BreedIdentifier = breedCode
        };
    }
}