using Npgsql;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;

public static class PostgresQueryUtilities
{
    public static async Task<List<Dictionary<string, object?>>> QueryRowsAsync(
        string connectionString,
        string tableName,
        string orderBy)
    {
        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var rows = new List<Dictionary<string, object?>>();

        var sql = $"SELECT * FROM {tableName} ORDER BY {orderBy}";
        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);

            rows.Add(row);
        }

        return rows;
    }
}