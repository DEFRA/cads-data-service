using Npgsql;
using Testcontainers.PostgreSql;

namespace Cads.Cds.Api.Tests.Integration;

public class PostgresFixture : IAsyncLifetime
{
    public PostgreSqlContainer? Container { get; private set; }

    public string NetworkName { get; } = "integration-test-network";
    private const string CadsDatabaseName = "cads"; // not needed?
    private const string POSTGRES_USER = "postgres";
    private const string POSTGRES_PASSWORD = "password";

    public static string ConnectionString =>
        $"Host=postgres;Port=5432;Database={CadsDatabaseName};Username={POSTGRES_USER};Password={POSTGRES_PASSWORD}";

    private string ConnectionStringWithoutDatabase =>
        $"Host=127.0.0.1;Port=5432;Database=postgres;Username={POSTGRES_USER};Password={POSTGRES_PASSWORD}";
    //TODO readonly connection

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(NetworkName);

        Container = new PostgreSqlBuilder("postgres:16.6")
            .WithName("postgres")
            .WithPortBinding(5432, 5432)
            .WithEnvironment("POSTGRES_USER", POSTGRES_USER)
            .WithEnvironment("POSTGRES_PASSWORD", POSTGRES_PASSWORD)
            .WithEnvironment("Postgres__DefaultConnection", ConnectionString)
            .WithEnvironment("Postgres__ReadOnlyConnection", ConnectionString)
            .WithNetwork(NetworkName)
            .WithNetworkAliases("postgres")
            .Build();

        await Container.StartAsync();
        var conn = Container.GetConnectionString();

        InitialiseDatabaseSchema();
        // possible TODO verify initialised
    }

    public async ValueTask DisposeAsync()
    {
        await Container!.DisposeAsync();
    }

    private void InitialiseDatabaseSchema()
    {
        using var connection = new NpgsqlConnection(ConnectionStringWithoutDatabase);
        var createDbCommand = new NpgsqlCommand( //TODO replace with trycatch and if not exists
            @$"
            CREATE DATABASE {CadsDatabaseName}
                WITH OWNER = postgres
                ENCODING = 'UTF8'
                CONNECTION LIMIT = -1",
            connection);

        connection.Open();
        createDbCommand.ExecuteNonQuery();
        connection.Close();
    }
}