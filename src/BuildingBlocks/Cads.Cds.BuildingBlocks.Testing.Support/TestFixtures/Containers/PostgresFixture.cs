using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

public class PostgresFixture : IAsyncLifetime
{
    public PostgreSqlContainer? Container { get; private set; }

    public static string ConnectionString =>
        $"Host=postgres;Port=5432;Database={TestDatabaseConstants.TestCadsDatabaseName};Username={TestDatabaseConstants.PostgresUserName};Password={TestDatabaseConstants.PostgresPassword}";

    public static string ReadConnectionString =>
        $"Host=postgres;Port=5432;Database={TestDatabaseConstants.TestCadsDatabaseName};Username={TestDatabaseConstants.PostgresReadUser};Password={TestDatabaseConstants.PostgresReadPassword}";

    private static string InitialisationConnectionString =>
        $"Host=127.0.0.1;Port=5432;Database=postgres;Username={TestDatabaseConstants.PostgresUserName};Password={TestDatabaseConstants.PostgresPassword}";

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(TestContainerConstants.NetworkName);

        Container = new PostgreSqlBuilder("postgres:16.6")
            .WithName("postgres")
            .WithPortBinding(5432, 5432)
            .WithEnvironment("POSTGRES_USER", TestDatabaseConstants.PostgresUserName)
            .WithEnvironment("POSTGRES_PASSWORD", TestDatabaseConstants.PostgresPassword)
            .WithNetwork(TestContainerConstants.NetworkName)
            .WithNetworkAliases("postgres")
            .Build();

        await Container.StartAsync();
        Container.GetConnectionString();

        InitialiseDatabaseSchema();
    }

    public async ValueTask DisposeAsync()
    {
        Exception? error = null;

        async ValueTask Safe(Func<ValueTask> f)
        {
            try { await f(); }
            catch (Exception ex) { error ??= ex; }
        }

        await Safe(() => Container!.DisposeAsync());

        GC.SuppressFinalize(this);

        if (error is not null)
            throw error;
    }

    private static void InitialiseDatabaseSchema()
    {
        using var connection = new NpgsqlConnection(InitialisationConnectionString);
        var createDbCommand = new NpgsqlCommand(@$"
            CREATE DATABASE ""{TestDatabaseConstants.TestCadsDatabaseName}""
                WITH OWNER = postgres
                ENCODING = 'UTF8'
                CONNECTION LIMIT = -1",
            connection);

        var createROUserCommand = new NpgsqlCommand(@$"
            CREATE ROLE ""{TestDatabaseConstants.PostgresReadUser}"" WITH LOGIN PASSWORD '{TestDatabaseConstants.PostgresReadPassword}';
            GRANT CONNECT ON DATABASE ""{TestDatabaseConstants.TestCadsDatabaseName}"" TO ""{TestDatabaseConstants.PostgresReadUser}"";
            GRANT USAGE ON SCHEMA public TO ""{TestDatabaseConstants.PostgresReadUser}"";
            GRANT SELECT ON ALL TABLES IN SCHEMA public TO ""{TestDatabaseConstants.PostgresReadUser}"";
            REVOKE CREATE ON SCHEMA public FROM PUBLIC;
            ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO ""{TestDatabaseConstants.PostgresReadUser}"";",
            connection);

        connection.Open();
        createDbCommand.ExecuteNonQuery();
        createROUserCommand.ExecuteNonQuery();
        connection.Close();

        ApplyDatabaseMigrations();
    }

    private static void ApplyDatabaseMigrations()
    {
    }
}