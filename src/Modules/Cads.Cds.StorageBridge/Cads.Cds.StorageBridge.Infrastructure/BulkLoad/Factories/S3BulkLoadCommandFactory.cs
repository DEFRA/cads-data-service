using Cads.Cds.StorageBridge.Application.Extensions;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public class S3BulkLoadCommandFactory(NpgsqlConnection connection) : IS3BulkLoadCommandFactory
{
    public NpgsqlConnection Connection { get; } = connection ?? throw new ArgumentNullException(nameof(connection));

    private static readonly NpgsqlCommandBuilder s_commandBuilder = new();

    /// <summary>
    /// Currently only "D" (Data) is used; no delete semantics yet.
    /// </summary>
    private static class RecordType
    {
        public const string Data = "D";
    }

    public DbCommand CreateTempTableCommand(BulkLoadDataTypes bulkImportType)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);

        return new NpgsqlCommand
        {
            CommandText =
                $"CREATE TEMP TABLE {tempTableName} (LIKE {tableName} INCLUDING ALL) ON COMMIT DROP;",
            Connection = Connection
        };
    }

    public StreamWriter CreateTextImport(BulkLoadDataTypes bulkImportType, char delimiter) =>
        Connection.BeginTextImport(
            $"COPY {GetTableName(bulkImportType, isTemp: true)} " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER true)");

    public StreamWriter CreateTextImport(BulkLoadDataTypes bulkImportType, char delimiter, IEnumerable<string> columns) =>
        Connection.BeginTextImport(
            $"COPY {GetTableName(bulkImportType, isTemp: true)} ({string.Join(",", columns)}) " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER false)");

    public async Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText =
                $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
                $"SELECT {string.Join(",", columnNames)} FROM {tempTableName}",
            Connection = Connection
        };
    }

    public async Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return new NpgsqlCommand
        {
            CommandText =
                $"UPDATE {tableName} AS m SET {string.Join(", ", columnNames.Select(col => $"m.{col} = t.{col}"))} " +
                $"FROM {tempTableName} AS t WHERE m.{key} = t.{key}",
            Connection = Connection
        };
    }

    public async Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return new NpgsqlCommand
        {
            CommandText =
                $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
                $"SELECT {string.Join(",", columnNames)} FROM {tempTableName} " +
                $"ON CONFLICT ({key}) DO UPDATE SET {string.Join(", ", columnNames.Select(c => $"{c} = EXCLUDED.{c}"))}",
            Connection = Connection
        };
    }

    public async Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return new NpgsqlCommand
        {
            CommandText = $"DELETE FROM {tableName} WHERE {key} IN (SELECT {key} FROM {tempTableName})",
            Connection = Connection
        };
    }

    public async Task<List<string>> FilterColumnsToTableAsync(
        BulkLoadDataTypes bulkImportType,
        IEnumerable<string> fileColumns,
        CancellationToken cancellationToken = default)
    {
        var dbColumns = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return [.. fileColumns.Where(c => dbColumns.Contains(c, StringComparer.OrdinalIgnoreCase))];
    }

    public string GetTableName(BulkLoadDataTypes bulkImportType, bool isTemp = false)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        return s_commandBuilder.QuoteIdentifier(isTemp ? $"temp_{tableName}" : tableName);
    }

    public virtual async Task<List<string>> GetColumnNamesAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        var columnNames = new List<string>();

        const string query = @"
            SELECT column_name 
            FROM information_schema.columns
            WHERE table_name = @tableName
            ORDER BY ordinal_position";

        await using var command = new NpgsqlCommand(query, Connection);
        command.Parameters.AddWithValue("tableName", tableName);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
            columnNames.Add(reader.GetString(0));

        return columnNames;
    }

    public async Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = string.Join(',', await GetColumnNamesAsync(bulkImportType, cancellationToken));

        return new NpgsqlCommand
        {
            CommandText = $"SELECT {columnNames} FROM {tempTableName}",
            Connection = Connection
        };
    }
}