using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.StorageBridge.Application.Extensions;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public class S3BulkLoadCommandFactory(NpgsqlConnection connection) : IS3BulkLoadCommandFactory
{
    private readonly NpgsqlConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    private static readonly NpgsqlCommandBuilder s_commandBuilder = new();

    /// <summary>
    /// Currently only "D" (Data) is used; no delete semantics yet.
    /// </summary>
    private static class RecordType
    {
        public const string Data = "D";
    }

    protected virtual string GenerateTempTableSql(BulkLoadDataType bulkImportType)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        return $"CREATE TEMP TABLE {tempTableName} (LIKE {tableName} INCLUDING ALL) ON COMMIT DROP;";
    }

    protected virtual async Task<string> GenerateInsertSqlAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
               $"SELECT {string.Join(",", columnNames)} FROM {tempTableName}";
    }

    protected virtual async Task<string> GenerateUpdateSqlAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return $"UPDATE {tableName} AS m SET {string.Join(", ", columnNames.Select(col => $"m.{col} = t.{col}"))} " +
               $"FROM {tempTableName} AS t WHERE m.{key} = t.{key}";
    }

    protected virtual async Task<string> GenerateUpsertSqlAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
               $"SELECT {string.Join(",", columnNames)} FROM {tempTableName} " +
               $"ON CONFLICT ({key}) DO UPDATE SET {string.Join(", ", columnNames.Select(c => $"{c} = EXCLUDED.{c}"))}";
    }

    protected virtual async Task<string> GenerateDeleteSqlAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return $"DELETE FROM {tableName} WHERE {key} IN (SELECT {key} FROM {tempTableName})";
    }

    protected virtual async Task<string> GenerateTempTableQuerySqlAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken)
    {
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = string.Join(',', await GetColumnNamesAsync(bulkImportType, cancellationToken));
        return $"SELECT {columnNames} FROM {tempTableName}";
    }

    public DbCommand CreateTempTableCommand(BulkLoadDataType bulkImportType)
    {
        var sql = GenerateTempTableSql(bulkImportType);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public StreamWriter CreateTextImport(BulkLoadDataType bulkImportType, char delimiter, IEnumerable<string> columns)
    {
        return _connection.BeginTextImport(
            $"COPY {GetTableName(bulkImportType, isTemp: true)} ({string.Join(",", columns)}) " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER false)");
    }

    public async Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateInsertSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpdateSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpsertSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateDeleteSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    [ExcludeFromCodeCoverage]
    public async Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateTempTableQuerySqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public string GetTableName(BulkLoadDataType bulkImportType, bool isTemp = false)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        // Temp tables live in the session-local pg_temp schema, so they must not be schema-qualified.
        if (isTemp)
            return s_commandBuilder.QuoteIdentifier($"temp_{tableName}");

        var schema = bulkImportType.GetTableSchema()?.GetDescription();

        return string.IsNullOrWhiteSpace(schema)
            ? s_commandBuilder.QuoteIdentifier(tableName)
            : $"{s_commandBuilder.QuoteIdentifier(schema)}.{s_commandBuilder.QuoteIdentifier(tableName)}";
    }

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB. Made virtual so can use test friendly factory.
    /// </summary>
    /// <param name="bulkImportType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [ExcludeFromCodeCoverage]
    public virtual async Task<List<string>> GetColumnNamesAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        var schema = bulkImportType.GetTableSchema()?.GetDescription();

        var columnNames = new List<string>();

        var query = @"
            SELECT column_name 
            FROM information_schema.columns
            WHERE table_name = @tableName
              AND (@schema IS NULL OR table_schema = @schema)
            ORDER BY ordinal_position";

        await using var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("tableName", tableName);
        command.Parameters.AddWithValue("schema", (object?)schema ?? DBNull.Value);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
            columnNames.Add(reader.GetString(0));

        return columnNames;
    }

    public async Task<List<string>> FilterColumnsToTableAsync(
        BulkLoadDataType bulkImportType,
        IEnumerable<string> fileColumns,
        CancellationToken cancellationToken = default)
    {
        var dbColumns = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return [.. fileColumns.Where(c => dbColumns.Contains(c, StringComparer.OrdinalIgnoreCase))];
    }
}