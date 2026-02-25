using System.Diagnostics.CodeAnalysis;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Configuration;
using Npgsql;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;

public sealed class PostgresDataSourceFactory(PostgresConfiguration config, IPostgresIamTokenGeneratorService? iamTokenGenerator = null) : IPostgresDataSourceFactory, IDisposable
{
    private readonly Dictionary<string, NpgsqlDataSource> _dataSources = new();
    private readonly SemaphoreSlim _lock = new(1, 1);
    private bool _disposed;

    public const string DefaultConnectionIdentifier = "Default";
    public const string ReadOnlyConnectionIdentifier = "ReadOnly";

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
            DefaultConnectionIdentifier => config.DefaultConnection,
            ReadOnlyConnectionIdentifier => config.ReadOnlyConnection,
            _ => throw new ArgumentException($"Unknown connection identifier: {connectionIdentifier}")
        };

        return NpgsqlDataSource.Create(connectionString);
    }

    private NpgsqlDataSource CreateIamAuthDataSource(string connectionIdentifier)
    {
        var host = connectionIdentifier switch
        {
            DefaultConnectionIdentifier => config.DefaultHost,
            ReadOnlyConnectionIdentifier => config.ReadOnlyHost,
            _ => throw new ArgumentException($"Unknown connection identifier: {connectionIdentifier}")
        };

        var builder = new NpgsqlDataSourceBuilder
        {
            ConnectionStringBuilder =
            {
                Host = host,
                Port = config.Port,
                Database = config.Name,
                Username = config.User,
                SslMode = SslMode.Require // AWS RDS requires SSL
             }
        };

        // Register password provider that generates IAM tokens
        builder.UsePeriodicPasswordProvider(
            passwordProvider: async (_, ct) =>
            {
                var token = await iamTokenGenerator!.GenerateAuthTokenAsync(
                    config.DefaultHost!,
                    config.Port,
                    config.User!);
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