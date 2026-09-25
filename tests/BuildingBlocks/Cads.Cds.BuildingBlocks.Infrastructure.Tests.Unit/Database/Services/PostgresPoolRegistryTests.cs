using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Database.Services;

public class PostgresPoolRegistryTests
{
    private readonly PostgresPoolRegistry _sut = new();

    [Theory]
    [InlineData(PostgresPools.Default, false)]
    [InlineData(PostgresPools.ReadOnly, true)]
    [InlineData(PostgresPools.ApiWrite, false)]
    [InlineData(PostgresPools.ApiRead, true)]
    [InlineData(PostgresPools.CadsGraphQLRead, true)]
    [InlineData(PostgresPools.HealthCheckWrite, false)]
    public void IsReadPool_Returns_Pool_Direction(string identifier, bool expected)
    {
        _sut.IsReadPool(identifier).Should().Be(expected);
    }

    [Fact]
    public void Identifiers_Are_Matched_Case_Insensitively()
    {
        _sut.IsKnown("cadsgraphqlread").Should().BeTrue();
    }

    [Fact]
    public void Unknown_Identifier_Is_Not_Known()
    {
        _sut.IsKnown("Nope").Should().BeFalse();
    }

    [Fact]
    public void IsReadPool_Throws_For_Unknown_Identifier()
    {
        var act = () => _sut.IsReadPool("Nope");

        act.Should().Throw<ArgumentException>().WithMessage("Unknown Postgres pool identifier 'Nope'*");
    }

    [Fact]
    public void Every_PostgresPools_Constant_Is_Registered()
    {
        var constants = typeof(PostgresPools).GetFields()
            .Where(f => f.IsLiteral)
            .Select(f => (string)f.GetRawConstantValue()!);

        _sut.Identifiers.Should().BeEquivalentTo(constants);
    }
}