using AutoFixture;
using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Testing.Support.Specimens.Builders;

namespace Cads.Cds.Api.Testing.Support.Specimens.Factories;

public class AnimalOnHoldingDataFactory
{
    public List<AnimalOnHolding> CreateMockData()
    {
        var animals = new List<AnimalOnHolding>();

        animals.AddRange(Create(TestBovineConstants.KnownCph, "Oakfield Farm",
            TestBovineConstants.KnownCphAnimalCount, idOffset: 0));

        // A second holding proves the CPH filter isolates results
        animals.AddRange(Create(TestBovineConstants.OtherCph, "Elm Farm",
            TestBovineConstants.OtherCphAnimalCount, idOffset: 1000));

        return animals;
    }

    // New fixture per holding so each gets its own builder instance
    private static List<AnimalOnHolding> Create(string cph, string locationName, int count, int idOffset)
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new AnimalOnHoldingBuilder(cph, locationName, idOffset));

        return [.. fixture.CreateMany<AnimalOnHolding>(count)];
    }
}