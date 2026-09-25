using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using FluentAssertions;
using Moq;
using Npgsql;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Database.Factories;

public class PostgresDataSourceFactoryTests
{
    private const string WriterHost = "writer.cluster.local";
    private const string ReaderHost = "reader.cluster-ro.local";
    private const string DefaultConnection = "Host=writer-cs;Database=test;Username=postgres";
    private static readonly PostgresPoolRegistry PoolRegistry = new();

    private const string ReadOnlyConnection = "Host=reader-cs;Database=test;Username=postgres";

    public static TheoryData<string> AllPools => [.. new PostgresPoolRegistry().Identifiers];

    public static TheoryData<string> ReadPools =>
    [
        PostgresPools.ReadOnly,
        PostgresPools.ApiRead,
        PostgresPools.MiBffRead,
        PostgresPools.StorageBridgeRead,
        PostgresPools.SystemAdminRead,
        PostgresPools.CadsGraphQLRead,
        PostgresPools.CtsGraphQLRead,
        PostgresPools.CtsTransactionsGraphQLRead,
        PostgresPools.CtsAuditGraphQLRead,
        PostgresPools.HealthCheckRead
    ];

    public static TheoryData<string> WritePools =>
    [
        PostgresPools.Default,
        PostgresPools.ApiWrite,
        PostgresPools.MiBffWrite,
        PostgresPools.StorageBridgeWrite,
        PostgresPools.SystemAdminWrite,
        PostgresPools.HealthCheckWrite
    ];

    private static PostgresConfiguration CreateConfig(
        bool useIam,
        Dictionary<string, PostgresPoolConfiguration>? pools = null) => new()
        {
            UseIamAuthentication = useIam,
            DefaultConnection = DefaultConnection,
            ReadOnlyConnection = ReadOnlyConnection,
            DefaultHost = WriterHost,
            ReadOnlyHost = ReaderHost,
            Port = 5432,
            Name = "cads",
            User = "cads_user",
            Pools = pools ?? []
        };

    [Theory]
    [MemberData(nameof(AllPools))]
    public void Known_Identifiers_Resolve_Without_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), PoolRegistry);

        var dataSource = sut.CreateDataSource(identifier);

        dataSource.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(AllPools))]
    public void Known_Identifiers_Resolve_With_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), PoolRegistry, Mock.Of<IPostgresIamTokenGeneratorService>());

        var dataSource = sut.CreateDataSource(identifier);

        dataSource.Should().NotBeNull();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Unknown_Identifier_Throws_With_Identifier_In_Message(bool useIam)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam), PoolRegistry, Mock.Of<IPostgresIamTokenGeneratorService>());

        var act = () => sut.CreateDataSource("Nope");

        act.Should().Throw<ArgumentException>().WithMessage("Unknown Postgres pool identifier 'Nope'*");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Pool_Sizes_And_ApplicationName_Are_Applied(bool useIam)
    {
        var config = CreateConfig(useIam, new()
        {
            [PostgresPools.ApiRead] = new() { ApplicationName = "cads-api-read", MaximumPoolSize = 40, MinimumPoolSize = 4 }
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.ApiRead);

        builder.ApplicationName.Should().Be("cads-api-read");
        builder.MaxPoolSize.Should().Be(40);
        builder.MinPoolSize.Should().Be(4);
    }

    [Fact]
    public void Npgsql_Defaults_Are_Kept_When_Pool_Settings_Are_Not_Configured()
    {
        var defaults = new NpgsqlConnectionStringBuilder();
        var config = CreateConfig(useIam: false, new()
        {
            [PostgresPools.ApiRead] = new()
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.ApiRead);

        builder.ApplicationName.Should().Be(defaults.ApplicationName);
        builder.MaxPoolSize.Should().Be(defaults.MaxPoolSize);
        builder.MinPoolSize.Should().Be(defaults.MinPoolSize);
    }

    [Fact]
    public void Pool_Keys_Are_Matched_Case_Insensitively()
    {
        var config = CreateConfig(useIam: false, new()
        {
            ["CadsGraphQlRead"] = new() { MaximumPoolSize = 3 }
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.CadsGraphQLRead);

        builder.MaxPoolSize.Should().Be(3);
    }

    [Fact]
    public void Pool_With_Zero_MaximumPoolSize_Is_Disabled()
    {
        var config = CreateConfig(useIam: false, new()
        {
            [PostgresPools.CtsGraphQLRead] = new() { MaximumPoolSize = 0 }
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry);

        var act = () => sut.CreateDataSource(PostgresPools.CtsGraphQLRead);

        act.Should().Throw<InvalidOperationException>().WithMessage("Postgres pool 'CtsGraphQLRead' is disabled*");
    }

    [Theory]
    [MemberData(nameof(ReadPools))]
    public void Read_Pools_Use_Reader_Host_With_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), PoolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be(ReaderHost);
    }

    [Theory]
    [MemberData(nameof(WritePools))]
    public void Write_Pools_Use_Writer_Host_With_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), PoolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be(WriterHost);
    }

    [Theory]
    [MemberData(nameof(ReadPools))]
    public void Read_Pools_Use_ReadOnlyConnection_Without_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), PoolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be("reader-cs");
    }

    [Theory]
    [MemberData(nameof(WritePools))]
    public void Write_Pools_Use_DefaultConnection_Without_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), PoolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be("writer-cs");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Pool_Host_Overrides_Default_Host(bool useIam)
    {
        var config = CreateConfig(useIam, new()
        {
            [PostgresPools.MiBffRead] = new() { Host = "mibff-replica.local" }
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.MiBffRead);

        builder.Host.Should().Be("mibff-replica.local");
        builder.Database.Should().Be(useIam ? "cads" : "test");
    }

    [Fact]
    public void Pool_ConnectionString_Overrides_Default_Connection_Without_Iam()
    {
        var config = CreateConfig(useIam: false, new()
        {
            [PostgresPools.StorageBridgeWrite] = new() { ConnectionString = "Host=bridge-db;Database=bridge;Username=bridge" }
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.StorageBridgeWrite);

        builder.Host.Should().Be("bridge-db");
        builder.Database.Should().Be("bridge");
        builder.Username.Should().Be("bridge");
    }

    [Theory]
    [InlineData(PostgresPools.ApiRead, ReaderHost)]
    [InlineData(PostgresPools.ApiWrite, WriterHost)]
    public async Task Iam_Token_Is_Generated_For_Resolved_Host(string identifier, string expectedHost)
    {
        var tokenGenerator = new Mock<IPostgresIamTokenGeneratorService>();
        tokenGenerator
            .Setup(x => x.GenerateAuthTokenAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync("token");
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), PoolRegistry, tokenGenerator.Object);

        var host = sut.BuildConnectionStringBuilder(identifier).Host!;
        var token = await sut.CreateIamPasswordProvider(host)(new NpgsqlConnectionStringBuilder(), CancellationToken.None);

        token.Should().Be("token");
        tokenGenerator.Verify(x => x.GenerateAuthTokenAsync(expectedHost, 5432, "cads_user"), Times.Once);
    }

    [Fact]
    public async Task Iam_Token_Is_Generated_For_Overridden_Host()
    {
        var tokenGenerator = new Mock<IPostgresIamTokenGeneratorService>();
        tokenGenerator
            .Setup(x => x.GenerateAuthTokenAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync("token");
        var config = CreateConfig(useIam: true, new()
        {
            [PostgresPools.CadsGraphQLRead] = new() { Host = "graphql-replica.local" }
        });
        using var sut = new PostgresDataSourceFactory(config, PoolRegistry, tokenGenerator.Object);

        var host = sut.BuildConnectionStringBuilder(PostgresPools.CadsGraphQLRead).Host!;
        await sut.CreateIamPasswordProvider(host)(new NpgsqlConnectionStringBuilder(), CancellationToken.None);

        tokenGenerator.Verify(x => x.GenerateAuthTokenAsync("graphql-replica.local", 5432, "cads_user"), Times.Once);
    }

    [Fact]
    public void Same_Identifier_Returns_Cached_Data_Source()
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), PoolRegistry);

        var first = sut.CreateDataSource(PostgresPools.ApiRead);
        var second = sut.CreateDataSource(PostgresPools.ApiRead);

        second.Should().BeSameAs(first);
    }

    [Fact]
    public void Different_Identifiers_Return_Separate_Data_Sources()
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), PoolRegistry);

        var apiRead = sut.CreateDataSource(PostgresPools.ApiRead);
        var miBffRead = sut.CreateDataSource(PostgresPools.MiBffRead);

        miBffRead.Should().NotBeSameAs(apiRead);
    }

    [Fact]
    public void Legacy_Two_Pool_Config_Still_Resolves_Default_And_ReadOnly()
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), PoolRegistry);

        sut.BuildConnectionStringBuilder(PostgresDataSourceFactory.DefaultConnectionIdentifier).Host.Should().Be("writer-cs");
        sut.BuildConnectionStringBuilder(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier).Host.Should().Be("reader-cs");
    }
}