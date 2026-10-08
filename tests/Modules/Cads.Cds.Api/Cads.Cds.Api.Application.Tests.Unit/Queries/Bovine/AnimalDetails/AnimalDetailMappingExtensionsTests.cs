using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;
using Cads.Cds.Api.Core.Domain.Entities.Animals;
using FluentAssertions;

namespace Cads.Cds.Api.Application.Tests.Unit.Queries.Bovine.AnimalDetails;

public class AnimalDetailMappingExtensionsTests
{
    [Fact]
    public void ToDto_ShouldOmitParentsWithNoIdentifier()
    {
        var entity = new AnimalDetail { Identifier = "UK1", IdentifierSchema = "s", SireIdentifier = "UK2" };

        var dto = entity.ToDto();

        dto.AnimalDetail!.Parentage.Should().ContainSingle(p => p.Relationship == "Sire");
    }

    [Fact]
    public void ToDto_ShouldFallBackToIdentifierSchemaForParents()
    {
        var entity = new AnimalDetail { Identifier = "UK1", IdentifierSchema = "ear-tag", GeneticDamIdentifier = "UK3" };

        var dto = entity.ToDto();

        dto.AnimalDetail!.Parentage!.Single().AnimalIdentifier!.Schema.Should().Be("ear-tag");
    }

    [Fact]
    public void ToDto_ShouldParseEventDateTimeAsUtc()
    {
        var entity = new AnimalDetail { Identifier = "UK1", EventDatetime = "2026-08-24T12:00:00Z" };

        entity.ToDto().EventDateTime.Should().Be(new DateTime(2026, 08, 24, 12, 0, 0, DateTimeKind.Utc));
    }
}