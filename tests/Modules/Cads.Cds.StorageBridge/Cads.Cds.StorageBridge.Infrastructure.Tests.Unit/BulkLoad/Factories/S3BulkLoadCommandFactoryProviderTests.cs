using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;
using FluentAssertions;
using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.BulkLoad.Factories;

public class S3BulkLoadCommandFactoryProviderTests
{
    private readonly S3BulkLoadCommandFactoryProvider _sut = new();

    [Fact]
    public void Create_ShouldReturnConcreteFactoryWithSameConnection()
    {
        // Arrange
        var connection = new NpgsqlConnection("Host=cads-postgres;Port=5432;Database=cads_data_service;Username=postgres;Password=postgres");

        // Act
        var factory = _sut.Create(connection);

        // Assert
        factory.Should().BeOfType<S3BulkLoadCommandFactory>();

        var typed = (S3BulkLoadCommandFactory)factory;
        typed.Connection.Should().Be(connection);
    }
}