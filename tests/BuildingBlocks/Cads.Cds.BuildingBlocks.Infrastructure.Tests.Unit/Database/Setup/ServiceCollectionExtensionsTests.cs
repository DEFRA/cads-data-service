using Amazon.Runtime;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

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

    // A complete, valid set of settings for password-based authentication built from discrete settings.
    private static Dictionary<string, string> PasswordBasedSettings() => new()
    {
        ["Postgres:UsePasswordBasedAuthentication"] = "true",
        ["Postgres:DefaultHost"] = "writer",
        ["Postgres:ReadOnlyHost"] = "reader",
        ["Postgres:DefaultPort"] = "5432",
        ["Postgres:ReadOnlyPort"] = "5432",
        ["Postgres:Name"] = "cads",
        ["Postgres:User"] = "cads_user",
        ["Postgres:Password"] = "pw"
    };

    [Fact]
    public void Throws_When_Invalid_Connection_Identifier_Is_Used()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                (new Dictionary<string, string>
                {
                    ["Postgres:DefaultConnection"] = "Host=localhost;Database=test",
                    ["Postgres:ReadOnlyConnection"] = "Host=localhost;Database=test",
                    ["Postgres:UseIamAuthentication"] = "false"
                })
                .ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value)
            )
            .Build();
        var services = new ServiceCollection();
        services.AddLogging();
        services.ConfigureDatabase(configuration);
        services.AddPostgresDbContext<TestDbContext>("invalidConnectionIdentifier");

        var provider = services.BuildServiceProvider();

        var act = () => provider.GetRequiredService<TestDbContext>();
        act.Should().Throw<ArgumentException>().WithMessage("Unknown Postgres pool identifier 'invalidConnectionIdentifier'*");
    }

    private class TestDbContext(DbContextOptions<TestDbContext> options) : CadsDbContext(options)
    {
    }

    [Fact]
    public void Throws_When_Configuration_Section_Is_Missing()
    {
        var config = new Dictionary<string, string>();
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                (config ?? [])
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
            ["Postgres:ReadOnlyConnection"] = "Host=localhost;Database=test;",
            ["Postgres:UseIamAuthentication"] = "false"
        };

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Connection string 'DefaultConnection' not found or empty");
    }

    [Fact]
    public void Throws_When_ReadOnlyConnection_Is_Missing()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "",
            ["Postgres:UseIamAuthentication"] = "false"
        };

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Connection string 'ReadOnlyConnection' not found or empty");
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
            .WithMessage("IAM or password based authentication requires DefaultHost, ReadOnlyHost, Name, and User to be configured");
    }

    [Fact]
    public void Registers_PostgresConfiguration_And_Services_When_Using_Connection_String()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "Host=localhost-ro;Database=test;",
            ["Postgres:UseIamAuthentication"] = "false"
        };

        var provider = BuildProvider(config);

        var pgConfig = provider.GetRequiredService<PostgresConfiguration>();
        pgConfig.DefaultConnection.Should().Be("Host=localhost;Database=test;");
        pgConfig.ReadOnlyConnection.Should().Be("Host=localhost-ro;Database=test;");

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
            ["Postgres:DefaultHost"] = "localhost",
            ["Postgres:ReadOnlyHost"] = "localhost",
            ["Postgres:Name"] = "test",
            ["Postgres:User"] = "test"
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

        provider.GetRequiredService<IPostgresIamTokenGeneratorService>()
            .Should().BeOfType<PostgresIamTokenGeneratorService>();

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
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "Host=localhost;Database=test;"
        };

        var provider = BuildProvider(config);
        var context = provider.GetRequiredService<HealthCheckDbContext>();

        context.Should().NotBeNull();

        var connection = context.Database.GetDbConnection();
        connection.ConnectionString.Should().Be("Host=localhost;Database=test");
    }

    [Fact]
    public void Throws_When_Unknown_Pool_Is_Configured()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "Host=localhost;Database=test;",
            ["Postgres:Pools:ApiRead:MaximumPoolSize"] = "10",
            ["Postgres:Pools:ApiReed:MaximumPoolSize"] = "10"
        };

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Unknown Postgres pool(s) configured under 'Postgres:Pools': ApiReed*");
    }

    [Fact]
    public void Binds_Pool_Configuration()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=localhost;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "Host=localhost;Database=test;",
            ["Postgres:Pools:ApiRead:ApplicationName"] = "cads-api-read",
            ["Postgres:Pools:ApiRead:MaximumPoolSize"] = "40",
            ["Postgres:Pools:ApiRead:MinimumPoolSize"] = "4"
        };

        var provider = BuildProvider(config);

        var pool = provider.GetRequiredService<PostgresConfiguration>().Pools[PostgresPools.ApiRead];
        pool.ApplicationName.Should().Be("cads-api-read");
        pool.MaximumPoolSize.Should().Be(40);
        pool.MinimumPoolSize.Should().Be(4);
        pool.Host.Should().BeNull();
    }

    [Fact]
    public void Health_Check_Contexts_Use_Configured_Pools()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=writer;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "Host=reader;Database=test;",
            ["Postgres:Pools:HealthCheckWrite:ApplicationName"] = "cads-health-write",
            ["Postgres:Pools:HealthCheckRead:ApplicationName"] = "cads-health-read"
        };

        var provider = BuildProvider(config);

        var write = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckDbContext>().Database.GetDbConnection().ConnectionString);
        var read = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckReadOnlyDbContext>().Database.GetDbConnection().ConnectionString);

        write.Host.Should().Be("writer");
        write.ApplicationName.Should().Be("cads-health-write");
        read.Host.Should().Be("reader");
        read.ApplicationName.Should().Be("cads-health-read");
    }

    // ------------------------------------------------------------------
    // Password-based authentication built from discrete settings
    // (UsePasswordBasedAuthentication = true)
    // ------------------------------------------------------------------

    [Fact]
    public void Registers_PostgresConfiguration_And_Services_When_Using_Password_Based_Authentication()
    {
        using var provider = BuildProvider(PasswordBasedSettings());

        var pgConfig = provider.GetRequiredService<PostgresConfiguration>();
        pgConfig.UsePasswordBasedAuthentication.Should().BeTrue();
        pgConfig.UseIamAuthentication.Should().BeFalse();
        pgConfig.DefaultHost.Should().Be("writer");
        pgConfig.ReadOnlyHost.Should().Be("reader");
        pgConfig.DefaultPort.Should().Be(5432);
        pgConfig.ReadOnlyPort.Should().Be(5432);
        pgConfig.Name.Should().Be("cads");
        pgConfig.User.Should().Be("cads_user");
        pgConfig.Password.Should().Be("pw");

        provider.GetRequiredService<IPostgresDataSourceFactory>()
            .Should().BeOfType<PostgresDataSourceFactory>();

        provider.GetRequiredService<PostgresHealthCheck>()
            .Should().BeOfType<PostgresHealthCheck>();

        provider.GetRequiredService<IPostgresStatusService>()
            .Should().BeOfType<PostgresStatusService>();
    }

    [Fact]
    public void Password_Based_Authentication_Does_Not_Register_The_Iam_Token_Generator()
    {
        using var provider = BuildProvider(PasswordBasedSettings());

        provider.GetService<IPostgresIamTokenGeneratorService>().Should().BeNull();
    }

    [Fact]
    public void Password_Based_Authentication_Does_Not_Need_Connection_Strings()
    {
        // No DefaultConnection / ReadOnlyConnection keys at all.
        var config = PasswordBasedSettings();

        Action act = () => BuildProvider(config).Dispose();

        act.Should().NotThrow();
    }

    [Fact]
    public void Password_Based_Authentication_Ignores_Placeholder_Connection_Strings()
    {
        // This is what the deployed environments see: the appsettings.json placeholders are still present.
        var config = PasswordBasedSettings();
        config["Postgres:DefaultConnection"] = "Replace in docker-compose.override.yml";
        config["Postgres:ReadOnlyConnection"] = "Replace in docker-compose.override.yml";

        using var provider = BuildProvider(config);
        var context = provider.GetRequiredService<HealthCheckDbContext>();

        var connection = new NpgsqlConnectionStringBuilder(context.Database.GetDbConnection().ConnectionString);
        connection.Host.Should().Be("writer");
        connection.Database.Should().Be("cads");
    }

    [Fact]
    public void Registers_DbContext_Built_From_Settings_When_Using_Password_Based_Authentication()
    {
        using var provider = BuildProvider(PasswordBasedSettings());

        var write = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckDbContext>().Database.GetDbConnection().ConnectionString);
        var read = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckReadOnlyDbContext>().Database.GetDbConnection().ConnectionString);

        write.Host.Should().Be("writer");
        write.Port.Should().Be(5432);
        write.Database.Should().Be("cads");
        write.Username.Should().Be("cads_user");
        write.SslMode.Should().Be(SslMode.Require);
        read.Host.Should().Be("reader");
        read.Database.Should().Be("cads");
        read.Username.Should().Be("cads_user");
    }

    [Fact]
    public void Password_Is_Not_Exposed_Through_The_DbContext_Connection_String()
    {
        using var provider = BuildProvider(PasswordBasedSettings());

        var connectionString = provider.GetRequiredService<HealthCheckDbContext>().Database.GetDbConnection().ConnectionString;

        connectionString.Should().NotContain("pw");
        new NpgsqlConnectionStringBuilder(connectionString).Password.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Health_Check_Contexts_Use_Configured_Pools_When_Using_Password_Based_Authentication()
    {
        var config = PasswordBasedSettings();
        config["Postgres:Pools:HealthCheckWrite:ApplicationName"] = "cads-health-write";
        config["Postgres:Pools:HealthCheckRead:ApplicationName"] = "cads-health-read";

        using var provider = BuildProvider(config);

        var write = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckDbContext>().Database.GetDbConnection().ConnectionString);
        var read = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckReadOnlyDbContext>().Database.GetDbConnection().ConnectionString);

        write.Host.Should().Be("writer");
        write.ApplicationName.Should().Be("cads-health-write");
        read.Host.Should().Be("reader");
        read.ApplicationName.Should().Be("cads-health-read");
    }

    [Fact]
    public void Pool_Host_Override_Is_Applied_When_Using_Password_Based_Authentication()
    {
        var config = PasswordBasedSettings();
        config["Postgres:Pools:HealthCheckRead:Host"] = "health-replica";

        using var provider = BuildProvider(config);

        var read = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckReadOnlyDbContext>().Database.GetDbConnection().ConnectionString);
        read.Host.Should().Be("health-replica");
        read.Database.Should().Be("cads");
    }

    [Fact]
    public void Throws_When_Password_Based_Connection_Details_Are_Missing()
    {
        var config = new Dictionary<string, string>
        {
            ["Postgres:UsePasswordBasedAuthentication"] = "true"
        };

        Action act = () => BuildProvider(config);

        // Add .WithMessage("...") here once you want to pin the exact wording of your validation message.
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData("DefaultHost")]
    [InlineData("ReadOnlyHost")]
    [InlineData("Name")]
    [InlineData("User")]
    [InlineData("Password")]
    public void Throws_When_A_Password_Based_Setting_Is_Missing(string missing)
    {
        var config = PasswordBasedSettings();
        config.Remove($"Postgres:{missing}");

        Action act = () => BuildProvider(config);

        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData("DefaultHost")]
    [InlineData("ReadOnlyHost")]
    [InlineData("Name")]
    [InlineData("User")]
    [InlineData("Password")]
    public void Throws_When_A_Password_Based_Setting_Is_Blank(string blank)
    {
        var config = PasswordBasedSettings();
        config[$"Postgres:{blank}"] = "   ";

        Action act = () => BuildProvider(config);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Password_Setting_Is_Ignored_When_Password_Based_Authentication_Is_Off()
    {
        // A password is configured, but the flag is off, so the connection strings are what count.
        var config = new Dictionary<string, string>
        {
            ["Postgres:DefaultConnection"] = "Host=writer-cs;Database=test;",
            ["Postgres:ReadOnlyConnection"] = "Host=reader-cs;Database=test;",
            ["Postgres:UsePasswordBasedAuthentication"] = "false",
            ["Postgres:Password"] = "pw"
        };

        using var provider = BuildProvider(config);

        var write = new NpgsqlConnectionStringBuilder(
            provider.GetRequiredService<HealthCheckDbContext>().Database.GetDbConnection().ConnectionString);
        write.Host.Should().Be("writer-cs");
        write.Database.Should().Be("test");
    }

    [Fact]
    public void Password_Based_Authentication_Defaults_To_Off_And_Still_Requires_Connection_Strings()
    {
        // Nothing but a password and the discrete settings: with the flag unset, the standard rules apply.
        var config = PasswordBasedSettings();
        config.Remove("Postgres:UsePasswordBasedAuthentication");

        Action act = () => BuildProvider(config);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Connection string 'DefaultConnection' not found or empty");
    }
}