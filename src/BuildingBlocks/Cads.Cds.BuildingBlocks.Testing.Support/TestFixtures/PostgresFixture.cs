using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

public class PostgresFixture : IAsyncLifetime
{
    public PostgreSqlContainer? Container { get; private set; }

    public string NetworkName { get; } = "integration-test-network";
    private const string CadsDatabaseName = "cads";
    private const string PostgresUserName = "postgres";
    private const string PostgresPassword = "password";
    private const string PostgresReadUser = "readonlyuser";
    private const string PostgresReadPassword = "readonly";

    public static string ConnectionString =>
        $"Host=postgres;Port=5432;Database={CadsDatabaseName};Username={PostgresUserName};Password={PostgresPassword}";

    public static string ReadConnectionString =>
        $"Host=postgres;Port=5432;Database={CadsDatabaseName};Username={PostgresReadUser};Password={PostgresReadPassword}";

    private string InitialisationConnectionString =>
        $"Host=127.0.0.1;Port=5432;Database=postgres;Username={PostgresUserName};Password={PostgresPassword}";

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(NetworkName);

        Container = new PostgreSqlBuilder("postgres:16.6")
            .WithName("postgres")
            .WithPortBinding(5432, 5432)
            .WithEnvironment("POSTGRES_USER", PostgresUserName)
            .WithEnvironment("POSTGRES_PASSWORD", PostgresPassword)
            .WithNetwork(NetworkName)
            .WithNetworkAliases("postgres")
            .Build();

        await Container.StartAsync();
        var conn = Container.GetConnectionString();

        InitialiseDatabaseSchema();
    }

    public async ValueTask DisposeAsync()
    {
        await Container!.DisposeAsync();
    }

    private void InitialiseDatabaseSchema()
    {
        using var connection = new NpgsqlConnection(InitialisationConnectionString);
        var createDbCommand = new NpgsqlCommand(
            @$"
            CREATE DATABASE {CadsDatabaseName}
                WITH OWNER = postgres
                ENCODING = 'UTF8'
                CONNECTION LIMIT = -1",
            connection);

        var createROUserCommand = new NpgsqlCommand(
            @$"
            CREATE ROLE {PostgresReadUser} WITH LOGIN PASSWORD '{PostgresReadPassword}';
            GRANT CONNECT ON DATABASE {CadsDatabaseName} TO {PostgresReadUser};
            GRANT USAGE ON SCHEMA public TO {PostgresReadUser};
            GRANT SELECT ON ALL TABLES IN SCHEMA public TO {PostgresReadUser};
            REVOKE CREATE ON SCHEMA public FROM PUBLIC;
            ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO {PostgresReadUser};",
            connection);

        connection.Open();
        createDbCommand.ExecuteNonQuery();
        createROUserCommand.ExecuteNonQuery();
        connection.Close();
    }
}