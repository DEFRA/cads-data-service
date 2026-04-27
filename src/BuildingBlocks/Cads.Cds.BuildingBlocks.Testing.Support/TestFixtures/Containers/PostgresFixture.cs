using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using DotNet.Testcontainers.Builders;
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

    private string InitialisationConnectionString =>
        $"Host=127.0.0.1;Port={Container!.GetMappedPublicPort(5432)};Database=postgres;Username={TestDatabaseConstants.PostgresUserName};Password={TestDatabaseConstants.PostgresPassword}";

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(TestContainerConstants.NetworkName);

        Container = new PostgreSqlBuilder("postgres:16.6")
            .WithEnvironment("POSTGRES_USER", TestDatabaseConstants.PostgresUserName)
            .WithEnvironment("POSTGRES_PASSWORD", TestDatabaseConstants.PostgresPassword)
            .WithNetwork(TestContainerConstants.NetworkName)
            .WithNetworkAliases("postgres")
            .Build();

        await Container.StartAsync();
        Container.GetConnectionString();

        await InitialiseDatabaseSchema();
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

    private async Task InitialiseDatabaseSchema()
    {
        // 1. Connect to the postgres database to create the test DB + user
        using (var connection = new NpgsqlConnection(InitialisationConnectionString))
        {
            connection.Open();
            CreateDatabase(connection);
            CreateReadOnlyUser(connection);
            connection.Close();
        }

        // 2. Connect to the TEST database to apply permissions
        using (var connection = new NpgsqlConnection(
                   $"Host=127.0.0.1;Port={Container!.GetMappedPublicPort(5432)};" +
                   $"Database={TestDatabaseConstants.TestCadsDatabaseName};" +
                   $"Username={TestDatabaseConstants.PostgresUserName};" +
                   $"Password={TestDatabaseConstants.PostgresPassword}"))
        {
            connection.Open();
            GrantReadOnlyPermissions(connection);
            connection.Close();
        }

        // 3. Run Liquibase migrations (creates tables + views)
        await ApplyDatabaseMigrations();
    }

    private static void CreateDatabase(NpgsqlConnection connection)
    {
        using var cmd = new NpgsqlCommand($@"
            CREATE DATABASE ""{TestDatabaseConstants.TestCadsDatabaseName}""
            WITH OWNER = {TestDatabaseConstants.PostgresUserName}
            ENCODING = 'UTF8'
            CONNECTION LIMIT = -1;
        ", connection);

        cmd.ExecuteNonQuery();
    }

    private static void CreateReadOnlyUser(NpgsqlConnection connection)
    {
        using var cmd = new NpgsqlCommand($@"
            DO $$
            BEGIN
                IF NOT EXISTS (
                    SELECT FROM pg_roles WHERE rolname = '{TestDatabaseConstants.PostgresReadUser}'
                ) THEN
                    CREATE ROLE ""{TestDatabaseConstants.PostgresReadUser}"" 
                        WITH LOGIN PASSWORD '{TestDatabaseConstants.PostgresReadPassword}';
                END IF;
            END
            $$;
        ", connection);

        cmd.ExecuteNonQuery();
    }

    private static void GrantReadOnlyPermissions(NpgsqlConnection connection)
    {
        using var cmd = new NpgsqlCommand($@"
            -- Allow read-only user to connect to the test DB
            GRANT CONNECT ON DATABASE ""{TestDatabaseConstants.TestCadsDatabaseName}""
                TO ""{TestDatabaseConstants.PostgresReadUser}"";

            -- Allow usage of the public schema
            GRANT USAGE ON SCHEMA public 
                TO ""{TestDatabaseConstants.PostgresReadUser}"";

            -- Grant SELECT on all existing tables and views
            GRANT SELECT ON ALL TABLES IN SCHEMA public 
                TO ""{TestDatabaseConstants.PostgresReadUser}"";

            -- Prevent accidental writes
            REVOKE CREATE ON SCHEMA public FROM PUBLIC;

            -- Ensure future tables/views created by Liquibase are readable
            ALTER DEFAULT PRIVILEGES IN SCHEMA public 
                GRANT SELECT ON TABLES TO ""{TestDatabaseConstants.PostgresReadUser}"";
        ", connection);

        cmd.ExecuteNonQuery();
    }

    private static async Task ApplyDatabaseMigrations()
    {
        // Path to your Liquibase changelog folder
        var changelogPath = FindChangelogFolder();

        var liquibaseContainer = new ContainerBuilder("liquibase/liquibase:5.0.1")
            .WithBindMount(changelogPath, "/liquibase/changelog")
            .WithCommand(
            [
            "sh", "-c",
            // Install PostgreSQL extension (required for Liquibase 5.x)
            "lpm add postgresql --global && " +
            // Run the update
            $"liquibase " +
            $"--url=jdbc:postgresql://postgres:5432/{TestDatabaseConstants.TestCadsDatabaseName} " +
            $"--username={TestDatabaseConstants.PostgresUserName} " +
            $"--password={TestDatabaseConstants.PostgresPassword} " +
            $"--searchPath=/liquibase/changelog " +
            $"--changelog-file=db.changelog.xml " +
            $"--liquibaseSchemaName=public " +
            $"--contexts=local update"
            ])
            .WithNetwork(TestContainerConstants.NetworkName)
            .Build();

        await liquibaseContainer.StartAsync();

        var exitCode = await liquibaseContainer.GetExitCodeAsync();
        if (exitCode != 0)
            throw new InvalidOperationException($"Liquibase migrations failed with exit code {exitCode}");
    }

    private static string FindChangelogFolder()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);

        while (dir != null)
        {
            var candidate = Path.Combine(dir.FullName, "changelog");
            if (Directory.Exists(candidate))
                return candidate;

            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate changelog folder.");
    }
}