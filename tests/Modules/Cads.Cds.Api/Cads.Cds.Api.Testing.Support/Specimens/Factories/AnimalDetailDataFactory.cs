using AutoFixture;
using Cads.Cds.Api.Core.Domain.Entities.Animals;
using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Testing.Support.Specimens.Builders;

namespace Cads.Cds.Api.Testing.Support.Specimens.Factories;

public class AnimalDetailDataFactory
{
    public static List<AnimalDetail> CreateMockData()
    {
        List<AnimalDetailSpec> specs =
        [
            new(TestBovineConstants.KnownIdentifier, HasParents: true, IsDead: false),
            new(TestBovineConstants.KnownIdentifierNoParents, HasParents: false, IsDead: true)
        ];

        var fixture = new Fixture();
        fixture.Customizations.Add(new AnimalDetailBuilder(specs));

        return [.. fixture.CreateMany<AnimalDetail>(specs.Count)];
    }
}