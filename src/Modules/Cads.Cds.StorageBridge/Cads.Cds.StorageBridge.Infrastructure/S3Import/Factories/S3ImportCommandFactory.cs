using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Application.Extensions;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

public class S3ImportCommandFactory(NpgsqlConnection connection) : IS3ImportCommandFactory
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

    protected virtual string GenerateTempTableSql(ImportDataType importDataType, SchemaName schemaName)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        return $"CREATE TEMP TABLE {tempTableName} (LIKE {tableName} INCLUDING ALL) ON COMMIT DROP;";
    }

    protected virtual async Task<string> GenerateInsertSqlAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var columnNames = await GetColumnNamesAsync(importDataType, schemaName, cancellationToken);

        return $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
               $"SELECT {string.Join(",", columnNames)} FROM {tempTableName}";
    }

    protected virtual async Task<string> GenerateUpdateSqlAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var columnNames = await GetColumnNamesAsync(importDataType, schemaName, cancellationToken);
        var key = importDataType.GetTableKey(schemaName) ?? columnNames[0];

        return $"UPDATE {tableName} AS m SET {string.Join(", ", columnNames.Select(col => $"m.{col} = t.{col}"))} " +
               $"FROM {tempTableName} AS t WHERE m.{key} = t.{key}";
    }

    protected virtual async Task<string> GenerateUpsertSqlAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var columnNames = await GetColumnNamesAsync(importDataType, schemaName, cancellationToken);
        var key = importDataType.GetTableKey(schemaName) ?? columnNames[0];

        return $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
               $"SELECT {string.Join(",", columnNames)} FROM {tempTableName} " +
               $"ON CONFLICT ({key}) DO UPDATE SET {string.Join(", ", columnNames.Select(c => $"{c} = EXCLUDED.{c}"))}";
    }

    protected virtual async Task<string> GenerateDeleteSqlAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var columnNames = await GetColumnNamesAsync(importDataType, schemaName, cancellationToken);
        var key = importDataType.GetTableKey(schemaName) ?? columnNames[0];

        return $"DELETE FROM {tableName} WHERE {key} IN (SELECT {key} FROM {tempTableName})";
    }

    protected virtual async Task<string> GenerateTempTableQuerySqlAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
    {
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var columnNames = string.Join(',', await GetColumnNamesAsync(importDataType, schemaName, cancellationToken));
        return $"SELECT {columnNames} FROM {tempTableName}";
    }

    public DbCommand CreateTempTableCommand(ImportDataType importDataType, SchemaName schemaName)
    {
        var sql = GenerateTempTableSql(importDataType, schemaName);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public StreamWriter CreateTextImport(ImportDataType importDataType, SchemaName schemaName, char delimiter, IEnumerable<string> columns)
    {
        return _connection.BeginTextImport(
            $"COPY {GetTableName(importDataType, schemaName, isTemp: true)} ({string.Join(",", columns)}) " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER false)");
    }

    public async Task<DbCommand> CreateInsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateInsertSqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateUpdateCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpdateSqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateUpsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpsertSqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateDeleteCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateDeleteSqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    [ExcludeFromCodeCoverage]
    public async Task<DbCommand> CreateTempTableQueryCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateTempTableQuerySqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public string GetTableName(ImportDataType importDataType, SchemaName schemaName, bool isTemp = false)
    {
        var tableName = importDataType.GetTableName(schemaName)
            ?? throw new ArgumentException("Table name cannot be null", nameof(importDataType));

        // Temp tables live in the session-local pg_temp schema, so they must not be schema-qualified.
        if (isTemp)
            return s_commandBuilder.QuoteIdentifier($"temp_{tableName}");

        var schema = schemaName.GetDescription();

        return string.IsNullOrWhiteSpace(schema)
            ? s_commandBuilder.QuoteIdentifier(tableName)
            : $"{s_commandBuilder.QuoteIdentifier(schema)}.{s_commandBuilder.QuoteIdentifier(tableName)}";
    }

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB. Made virtual so can use test friendly factory.
    /// </summary>
    /// <param name="importDataType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [ExcludeFromCodeCoverage]
    public virtual async Task<List<string>> GetColumnNamesAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var tableName = importDataType.GetTableName(schemaName)
            ?? throw new ArgumentException("Table name cannot be null", nameof(importDataType));

        var schema = schemaName.GetDescription();

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
        ImportDataType importDataType,
        SchemaName schemaName,
        IEnumerable<string> fileColumns,
        CancellationToken cancellationToken = default)
    {
        var dbColumns = await GetColumnNamesAsync(importDataType, schemaName, cancellationToken);

        return [.. fileColumns.Where(c => dbColumns.Contains(c, StringComparer.OrdinalIgnoreCase))];
    }
}