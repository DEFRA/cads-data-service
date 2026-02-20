using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Database.Setup;

public class ServiceCollectionExtensionsTests
{
    private static ServiceProvider BuildProvider(IDictionary<string, string> config)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                (config ?? new Dictionary<string, string>())
                    .ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value)
            )
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();
        services.ConfigureDatabase(configuration);

        return services.BuildServiceProvider();
    }

    [Fact]
    public void Throws_When_Configuration_Section_Is_Missing()
    {
        var config = new Dictionary<string, string>();
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                (config ?? new Dictionary<string, string>())
                    .ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value)
            )
            .Build();

        Action act = () => services.ConfigureDatabase(configuration);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Configuration section 'Postgres' is missing");
    }

    [Fact]
    public void Throws_When_DefaultConnection_Is_Missing()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = ""
        };

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Connection string 'DefaultConnection' not found or empty");
    }

    [Fact]
    public void Registers_PostgresConfiguration_And_Services()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;"
        };

        var provider = BuildProvider(config);

        var pgConfig = provider.GetRequiredService<PostgresConfiguration>();
        pgConfig.DefaultConnection.Should().Be("Host=localhost;Database=test;");

        provider.GetRequiredService<PostgresHealthCheck>()
            .Should().BeOfType<PostgresHealthCheck>();

        provider.GetRequiredService<IPostgresStatusService>()
            .Should().BeOfType<PostgresStatusService>();
    }

    [Fact]
    public void Registers_DbContext_With_Npgsql()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;"
        };

        var provider = BuildProvider(config);
        var context = provider.GetRequiredService<HealthCheckDbContext>();

        context.Should().NotBeNull();

        var connection = context.Database.GetDbConnection();
        connection.ConnectionString.Should().Be("Host=localhost;Database=test;");
    }
}