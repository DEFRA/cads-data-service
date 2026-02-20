using Amazon.Runtime;
using Amazon.Runtime.Credentials;
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
            ["Postgres:DefaultConnection"] = "",
            ["Postgres:UseIamAuthentication"] = "false"
        };

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Connection string 'DefaultConnection' not found or empty");
    }

    [Fact]
    public void Throws_When_IAM_Connection_Details_Are_Missing()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:UseIamAuthentication"] = "true"
        };

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("IAM authentication requires DbHost, DbName, and DbUser to be configured");
    }

    [Fact]
    public void Registers_PostgresConfiguration_And_Services_When_Using_Connection_String()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;",
            ["Postgres:UseIamAuthentication"] = "false"
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
    public void Registers_PostgresConfiguration_And_Services_When_Using_IAM()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:UseIamAuthentication"] = "true",
            ["Postgres:DbHost"] = "localhost",
            ["Postgres:DbName"] = "test",
            ["Postgres:DbUser"] = "test"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                config.ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value)
            )
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();

        // Add mock AWS credentials to prevent authentication errors in tests
        services.AddSingleton<AWSCredentials>(new BasicAWSCredentials("test", "test"));

        services.ConfigureDatabase(configuration);
        var provider = services.BuildServiceProvider();

        var pgConfig = provider.GetRequiredService<PostgresConfiguration>();
        pgConfig.Should().BeOfType<PostgresConfiguration>();

        provider.GetRequiredService<IPostgresIamTokenGenerator>()
            .Should().BeOfType<PostgresIamTokenGenerator>();

        provider.GetRequiredService<IPostgresDataSourceFactory>()
            .Should().BeOfType<PostgresDataSourceFactory>();

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
        connection.ConnectionString.Should().Be("Host=localhost;Database=test");
    }
}