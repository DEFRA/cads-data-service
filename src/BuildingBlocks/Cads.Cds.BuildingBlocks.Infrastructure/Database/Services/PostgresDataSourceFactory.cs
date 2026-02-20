using System.Diagnostics.CodeAnalysis;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Npgsql;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public sealed class PostgresDataSourceFactory(PostgresConfiguration config, IPostgresIamTokenGenerator? iamTokenGenerator = null) : IPostgresDataSourceFactory, IDisposable
{
    private readonly Dictionary<string, NpgsqlDataSource> _dataSources = new();
    private readonly SemaphoreSlim _lock = new(1, 1);
    private bool _disposed;

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

            NpgsqlDataSource dataSource;

            if (config.UseIamAuthentication)
            {
                dataSource = CreateIamAuthDataSource(connectionIdentifier);
            }
            else
            {
                dataSource = CreateStandardDataSource(connectionIdentifier);
            }

            _dataSources[connectionIdentifier] = dataSource;
            return dataSource;
        }
        finally
        {
            _lock.Release();
        }
    }

    private NpgsqlDataSource CreateStandardDataSource(string connectionIdentifier)
    {
        var connectionString = connectionIdentifier switch
        {
            "Default" => config.DefaultConnection,
            "ReadOnly" => config.ReadOnlyConnection ?? config.DefaultConnection,
            _ => throw new ArgumentException($"Unknown connection identifier: {connectionIdentifier}")
        };

        return NpgsqlDataSource.Create(connectionString);
    }

    private NpgsqlDataSource CreateIamAuthDataSource(string connectionIdentifier)
    {
        var builder = new NpgsqlDataSourceBuilder
        {
            ConnectionStringBuilder =
            {
                Host = config.DbHost,
                Port = config.DbPort,
                Database = config.DbName,
                Username = config.DbUser,
                SslMode = SslMode.Require // AWS RDS requires SSL
             }
        };

        // Register password provider that generates IAM tokens
        builder.UsePeriodicPasswordProvider(
            passwordProvider: async (_, ct) =>
            {
                var token = await iamTokenGenerator!.GenerateAuthTokenAsync(
                    config.DbHost!,
                    config.DbPort,
                    config.DbUser!);
                return token;
            },
            successRefreshInterval: TimeSpan.FromMinutes(10), // Refresh every 10 minutes
            failureRefreshInterval: TimeSpan.FromSeconds(30)  // Retry after 30 seconds on failure
        );

        return builder.Build();
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