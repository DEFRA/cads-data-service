using Npgsql;
using System.Data;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;

/// <summary>
/// A robust PostgresDb utility class that wraps ExecuteNonQuery, 
/// ExecuteScalar, and ExecuteQuery<T> with strongly‑typed mapping delegates.
/// </summary>
/// 
/// Examples
/// Insert / Update / Delete:
/// 
/// var db = new PostgresDb(connString);
///
/// int rows = await db.ExecuteNonQueryAsync(
///     "UPDATE users SET last_login = NOW() WHERE id = @id",
///     cmd => cmd.Parameters.AddWithValue("id", 42)
/// );
/// 
/// Scalar:
/// 
/// int count = await db.ExecuteScalarAsync<int>(
///    "SELECT COUNT(*) FROM users WHERE active = TRUE"
/// );
/// 
/// Query with mapping:
/// 
/// var users = await db.ExecuteQueryAsync(
///    "SELECT id, name FROM users WHERE active = TRUE",
///   reader => new User
///   {
///        Id = reader.GetInt32(0),
///        Name = reader.GetString(1)
///    }
/// );
public sealed class PostgresDb(string connectionString)
{
    private readonly string _connectionString = connectionString
            ?? throw new ArgumentNullException(nameof(connectionString));

    private async Task<NpgsqlConnection> CreateOpenConnectionAsync()
    {
        var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        return conn;
    }

    public async Task<int> ExecuteNonQueryAsync(
        string sql,
        Action<NpgsqlCommand>? configure = null)
    {
        await using var conn = await CreateOpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);

        configure?.Invoke(cmd);
        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<T?> ExecuteScalarAsync<T>(
        string sql,
        Action<NpgsqlCommand>? configure = null)
    {
        await using var conn = await CreateOpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);

        configure?.Invoke(cmd);
        object? result = await cmd.ExecuteScalarAsync();

        if (result == null || result is DBNull)
            return default;

        return (T)result;
    }

    public async Task<List<T>> ExecuteQueryAsync<T>(
        string sql,
        Func<NpgsqlDataReader, T> map,
        Action<NpgsqlCommand>? configure = null)
    {
        await using var conn = await CreateOpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);

        configure?.Invoke(cmd);

        var list = new List<T>();

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(map(reader));
        }

        return list;
    }

    public async Task<DataSet> FillDataSetAsync(
        string sql,
        Action<NpgsqlCommand>? configure = null)
    {
        await using var conn = await CreateOpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);

        configure?.Invoke(cmd);

        var dataSet = new DataSet();

        // DataAdapter is synchronous, so wrap in Task.Run
        await Task.Run(() =>
        {
            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dataSet);
        });

        return dataSet;
    }

    public async Task<T> PollUntilAsync<T>(
        string sql,
        Func<T, bool> condition,
        TimeSpan timeout,
        Action<NpgsqlCommand>? configure = null)
    {
        using var cts = new CancellationTokenSource(timeout);
        T result;
        do
        {
            result = (await ExecuteScalarAsync<T>(sql, configure))!;
            if (condition(result)) return result;
            await Task.Delay(500, cts.Token);
        }
        while (!cts.IsCancellationRequested);

        return result;
    }
}