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
    private const string TestCredsPw = "s3cret";
    private static readonly PostgresPoolRegistry s_poolRegistry = new();

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

    // Password authentication where only the discrete settings (and the password) are configured.
    // The full connection strings are left unset unless a test passes them in.
    private static PostgresConfiguration CreatePartsConfig(
        bool useIam = false,
        string? password = TestCredsPw,
        string? defaultConnection = null,
        string? readOnlyConnection = null,
        Dictionary<string, PostgresPoolConfiguration>? pools = null) => new()
        {
            UseIamAuthentication = useIam,
            DefaultConnection = defaultConnection!,
            ReadOnlyConnection = readOnlyConnection!,
            DefaultHost = WriterHost,
            ReadOnlyHost = ReaderHost,
            Port = 5432,
            Name = "cads",
            User = "cads_user",
            Password = password,
            Pools = pools ?? []
        };

    [Theory]
    [MemberData(nameof(AllPools))]
    public void Known_Identifiers_Resolve_Without_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), s_poolRegistry);

        var dataSource = sut.CreateDataSource(identifier);

        dataSource.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(AllPools))]
    public void Known_Identifiers_Resolve_With_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), s_poolRegistry, Mock.Of<IPostgresIamTokenGeneratorService>());

        var dataSource = sut.CreateDataSource(identifier);

        dataSource.Should().NotBeNull();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Unknown_Identifier_Throws_With_Identifier_In_Message(bool useIam)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam), s_poolRegistry, Mock.Of<IPostgresIamTokenGeneratorService>());

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var act = () => sut.CreateDataSource(PostgresPools.CtsGraphQLRead);

        act.Should().Throw<InvalidOperationException>().WithMessage("Postgres pool 'CtsGraphQLRead' is disabled*");
    }

    [Theory]
    [MemberData(nameof(ReadPools))]
    public void Read_Pools_Use_Reader_Host_With_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), s_poolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be(ReaderHost);
    }

    [Theory]
    [MemberData(nameof(WritePools))]
    public void Write_Pools_Use_Writer_Host_With_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), s_poolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be(WriterHost);
    }

    [Theory]
    [MemberData(nameof(ReadPools))]
    public void Read_Pools_Use_ReadOnlyConnection_Without_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), s_poolRegistry);

        sut.BuildConnectionStringBuilder(identifier).Host.Should().Be("reader-cs");
    }

    [Theory]
    [MemberData(nameof(WritePools))]
    public void Write_Pools_Use_DefaultConnection_Without_Iam(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), s_poolRegistry);

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

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
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: true), s_poolRegistry, tokenGenerator.Object);

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
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry, tokenGenerator.Object);

        var host = sut.BuildConnectionStringBuilder(PostgresPools.CadsGraphQLRead).Host!;
        await sut.CreateIamPasswordProvider(host)(new NpgsqlConnectionStringBuilder(), CancellationToken.None);

        tokenGenerator.Verify(x => x.GenerateAuthTokenAsync("graphql-replica.local", 5432, "cads_user"), Times.Once);
    }

    [Fact]
    public void Same_Identifier_Returns_Cached_Data_Source()
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), s_poolRegistry);

        var first = sut.CreateDataSource(PostgresPools.ApiRead);
        var second = sut.CreateDataSource(PostgresPools.ApiRead);

        second.Should().BeSameAs(first);
    }

    [Fact]
    public void Different_Identifiers_Return_Separate_Data_Sources()
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), s_poolRegistry);

        var apiRead = sut.CreateDataSource(PostgresPools.ApiRead);
        var miBffRead = sut.CreateDataSource(PostgresPools.MiBffRead);

        miBffRead.Should().NotBeSameAs(apiRead);
    }

    [Fact]
    public void Legacy_Two_Pool_Config_Still_Resolves_Default_And_ReadOnly()
    {
        using var sut = new PostgresDataSourceFactory(CreateConfig(useIam: false), s_poolRegistry);

        sut.BuildConnectionStringBuilder(PostgresDataSourceFactory.DefaultConnectionIdentifier).Host.Should().Be("writer-cs");
        sut.BuildConnectionStringBuilder(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier).Host.Should().Be("reader-cs");
    }

    // ------------------------------------------------------------------
    // Password authentication built from discrete settings
    // (Host / Port / Name / User / Password, with no full connection string)
    // ------------------------------------------------------------------

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Builds_Connection_From_Settings_When_Connection_String_Is_Not_Set(string? connectionString)
    {
        var config = CreatePartsConfig(defaultConnection: connectionString, readOnlyConnection: connectionString);
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.Default);

        builder.Host.Should().Be(WriterHost);
        builder.Port.Should().Be(5432);
        builder.Database.Should().Be("cads");
        builder.Username.Should().Be("cads_user");
        builder.Password.Should().Be(TestCredsPw);
    }

    [Theory]
    [MemberData(nameof(ReadPools))]
    public void Read_Pools_Use_ReadOnlyHost_When_Built_From_Settings(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreatePartsConfig(), s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(identifier);

        builder.Host.Should().Be(ReaderHost);
        builder.Password.Should().Be(TestCredsPw);
    }

    [Theory]
    [MemberData(nameof(WritePools))]
    public void Write_Pools_Use_DefaultHost_When_Built_From_Settings(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreatePartsConfig(), s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(identifier);

        builder.Host.Should().Be(WriterHost);
        builder.Password.Should().Be(TestCredsPw);
    }

    [Theory]
    [MemberData(nameof(AllPools))]
    public void Known_Identifiers_Resolve_From_Settings(string identifier)
    {
        using var sut = new PostgresDataSourceFactory(CreatePartsConfig(), s_poolRegistry);

        var dataSource = sut.CreateDataSource(identifier);

        dataSource.Should().NotBeNull();
    }

    [Fact]
    public void Connection_String_Wins_Over_Settings_When_Both_Are_Set()
    {
        var config = CreatePartsConfig(defaultConnection: DefaultConnection, readOnlyConnection: ReadOnlyConnection);
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var writer = sut.BuildConnectionStringBuilder(PostgresPools.Default);
        var reader = sut.BuildConnectionStringBuilder(PostgresPools.ReadOnly);

        writer.Host.Should().Be("writer-cs");
        writer.Database.Should().Be("test");
        writer.Password.Should().BeNullOrEmpty("the configured password must not be mixed into a full connection string");
        reader.Host.Should().Be("reader-cs");
    }

    [Fact]
    public void Each_Pool_Falls_Back_Independently_When_Only_One_Connection_String_Is_Set()
    {
        var config = CreatePartsConfig(defaultConnection: DefaultConnection, readOnlyConnection: null);
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var writer = sut.BuildConnectionStringBuilder(PostgresPools.Default);
        var reader = sut.BuildConnectionStringBuilder(PostgresPools.ReadOnly);

        writer.Host.Should().Be("writer-cs");
        writer.Password.Should().BeNullOrEmpty();
        reader.Host.Should().Be(ReaderHost);
        reader.Password.Should().Be(TestCredsPw);
    }

    [Fact]
    public void Pool_ConnectionString_Overrides_Settings()
    {
        var config = CreatePartsConfig(pools: new()
        {
            [PostgresPools.StorageBridgeWrite] = new() { ConnectionString = "Host=bridge-db;Database=bridge;Username=bridge;Password=bridge-pw" }
        });
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.StorageBridgeWrite);

        builder.Host.Should().Be("bridge-db");
        builder.Database.Should().Be("bridge");
        builder.Username.Should().Be("bridge");
        builder.Password.Should().Be("bridge-pw");
    }

    [Fact]
    public void Pool_Host_Overrides_Host_When_Built_From_Settings()
    {
        var config = CreatePartsConfig(pools: new()
        {
            [PostgresPools.MiBffRead] = new() { Host = "mibff-replica.local" }
        });
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.MiBffRead);

        builder.Host.Should().Be("mibff-replica.local");
        builder.Database.Should().Be("cads");
        builder.Password.Should().Be(TestCredsPw);
    }

    [Fact]
    public void Pool_Sizes_And_ApplicationName_Are_Applied_When_Built_From_Settings()
    {
        var config = CreatePartsConfig(pools: new()
        {
            [PostgresPools.ApiRead] = new() { ApplicationName = "cads-api-read", MaximumPoolSize = 40, MinimumPoolSize = 4 }
        });
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.ApiRead);

        builder.ApplicationName.Should().Be("cads-api-read");
        builder.MaxPoolSize.Should().Be(40);
        builder.MinPoolSize.Should().Be(4);
    }

    [Fact]
    public void Pool_With_Zero_MaximumPoolSize_Is_Disabled_When_Built_From_Settings()
    {
        var config = CreatePartsConfig(pools: new()
        {
            [PostgresPools.CtsGraphQLRead] = new() { MaximumPoolSize = 0 }
        });
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var act = () => sut.CreateDataSource(PostgresPools.CtsGraphQLRead);

        act.Should().Throw<InvalidOperationException>().WithMessage("Postgres pool 'CtsGraphQLRead' is disabled*");
    }

    [Fact]
    public void Connection_Built_From_Settings_Requires_Ssl_And_Sets_Keepalive_And_Timeout()
    {
        using var sut = new PostgresDataSourceFactory(CreatePartsConfig(), s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.Default);

        builder.SslMode.Should().Be(SslMode.Require);
        builder.CommandTimeout.Should().Be(60);
        builder.TcpKeepAlive.Should().BeTrue();
        builder.TcpKeepAliveTime.Should().Be(30);
    }

    [Fact]
    public void Same_Identifier_Returns_Cached_Data_Source_When_Built_From_Settings()
    {
        using var sut = new PostgresDataSourceFactory(CreatePartsConfig(), s_poolRegistry);

        var first = sut.CreateDataSource(PostgresPools.ApiRead);
        var second = sut.CreateDataSource(PostgresPools.ApiRead);

        second.Should().BeSameAs(first);
    }

    [Theory]
    [InlineData("simple")]
    [InlineData("has;semicolon")]
    [InlineData("has=equals")]
    [InlineData("it's")]
    [InlineData("say \"hi\"")]
    [InlineData("a'b\"c")]
    [InlineData("~?:$$x")]
    public void Password_With_Special_Characters_Round_Trips_Through_The_Connection_String(string password)
    {
        using var sut = new PostgresDataSourceFactory(CreatePartsConfig(password: password), s_poolRegistry);

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.Default);
        var reparsed = new NpgsqlConnectionStringBuilder(builder.ConnectionString);

        reparsed.Password.Should().Be(password);
    }

    [Fact]
    public void Password_Is_Not_Applied_With_Iam()
    {
        var config = CreatePartsConfig(useIam: true);
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry, Mock.Of<IPostgresIamTokenGeneratorService>());

        var builder = sut.BuildConnectionStringBuilder(PostgresPools.Default);

        builder.Host.Should().Be(WriterHost);
        builder.Password.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Non_Empty_Placeholder_Connection_String_Is_Parsed_And_Rejected()
    {
        // appsettings.json placeholders such as "Replace in docker-compose.override.yml" are not empty,
        // so they are treated as a real connection string and fail to parse. Remove the keys (or leave
        // them empty) in deployed environments that build the connection from settings.
        var config = CreatePartsConfig(
            defaultConnection: "Replace in docker-compose.override.yml",
            readOnlyConnection: "Replace in docker-compose.override.yml");
        using var sut = new PostgresDataSourceFactory(config, s_poolRegistry);

        var act = () => sut.BuildConnectionStringBuilder(PostgresPools.Default);

        act.Should().Throw<ArgumentException>();
    }
}