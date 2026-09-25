using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;

public sealed class PostgresDataSourceFactory(
    PostgresConfiguration config,
    IPostgresPoolRegistry poolRegistry,
    IPostgresIamTokenGeneratorService? iamTokenGenerator = null) : IPostgresDataSourceFactory, IDisposable
{
    private readonly Dictionary<string, NpgsqlDataSource> _dataSources = [];
    private readonly Dictionary<string, PostgresPoolConfiguration> _pools = new(config.Pools, StringComparer.OrdinalIgnoreCase);
    private readonly SemaphoreSlim _lock = new(1, 1);
    private bool _disposed;

    public const string DefaultConnectionIdentifier = PostgresPools.Default;
    public const string ReadOnlyConnectionIdentifier = PostgresPools.ReadOnly;

    public NpgsqlDataSource CreateDataSource(string connectionIdentifier)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        // Use cached data source if available
        if (_dataSources.TryGetValue(connectionIdentifier, out var existingDataSource))
        {
            return existingDataSource;
        }

        _lock.Wait();
        try
        {
            // Double-check after acquiring lock
            if (_dataSources.TryGetValue(connectionIdentifier, out existingDataSource))
            {
                return existingDataSource;
            }

            var connectionStringBuilder = BuildConnectionStringBuilder(connectionIdentifier);
            var builder = new NpgsqlDataSourceBuilder(connectionStringBuilder.ConnectionString);

            if (config.UseIamAuthentication)
            {
                // Register password provider that generates IAM tokens
                builder.UsePeriodicPasswordProvider(
                    passwordProvider: CreateIamPasswordProvider(connectionStringBuilder.Host!),
                    successRefreshInterval: TimeSpan.FromMinutes(10), // Refresh every 10 minutes
                    failureRefreshInterval: TimeSpan.FromSeconds(30)  // Retry after 30 seconds on failure
                );
            }

            var dataSource = builder.Build();

            _dataSources[connectionIdentifier] = dataSource;
            return dataSource;
        }
        finally
        {
            _lock.Release();
        }
    }

    internal NpgsqlConnectionStringBuilder BuildConnectionStringBuilder(string connectionIdentifier)
    {
        var isReadPool = poolRegistry.IsReadPool(connectionIdentifier);
        var pool = _pools.GetValueOrDefault(connectionIdentifier) ?? new PostgresPoolConfiguration();

        if (pool.MaximumPoolSize == 0)
        {
            throw new InvalidOperationException(
                $"Postgres pool '{connectionIdentifier}' is disabled (MaximumPoolSize is 0)");
        }

        var connectionStringBuilder = config.UseIamAuthentication
            ? CreateIamConnectionStringBuilder(pool, isReadPool)
            : CreateStandardConnectionStringBuilder(pool, isReadPool);

        ApplyPoolSettings(connectionStringBuilder, pool);

        return connectionStringBuilder;
    }

    internal Func<NpgsqlConnectionStringBuilder, CancellationToken, ValueTask<string>> CreateIamPasswordProvider(string host)
    {
        return async (_, _) => await iamTokenGenerator!.GenerateAuthTokenAsync(host, config.Port, config.User);
    }

    private NpgsqlConnectionStringBuilder CreateStandardConnectionStringBuilder(PostgresPoolConfiguration pool, bool isReadPool)
    {
        var connectionString = pool.ConnectionString ?? (isReadPool ? config.ReadOnlyConnection : config.DefaultConnection);
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

        if (pool.Host is not null)
        {
            connectionStringBuilder.Host = pool.Host;
        }

        return connectionStringBuilder;
    }

    private NpgsqlConnectionStringBuilder CreateIamConnectionStringBuilder(PostgresPoolConfiguration pool, bool isReadPool)
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = pool.Host ?? (isReadPool ? config.ReadOnlyHost : config.DefaultHost),
            Port = config.Port,
            Database = config.Name,
            Username = config.User,
            CommandTimeout = 60,
            TcpKeepAlive = true,
            TcpKeepAliveTime = 30,
            SslMode = SslMode.Require // AWS RDS requires SSL
        };
    }

    private static void ApplyPoolSettings(NpgsqlConnectionStringBuilder connectionStringBuilder, PostgresPoolConfiguration pool)
    {
        if (pool.ApplicationName is not null)
        {
            connectionStringBuilder.ApplicationName = pool.ApplicationName;
        }

        if (pool.MaximumPoolSize.HasValue)
        {
            connectionStringBuilder.MaxPoolSize = pool.MaximumPoolSize.Value;
        }

        if (pool.MinimumPoolSize.HasValue)
        {
            connectionStringBuilder.MinPoolSize = pool.MinimumPoolSize.Value;
        }
    }

    [ExcludeFromCodeCoverage]
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        foreach (var dataSource in _dataSources.Values)
        {
            dataSource.Dispose();
        }

        _dataSources.Clear();
        _lock.Dispose();

        _disposed = true;
    }
}