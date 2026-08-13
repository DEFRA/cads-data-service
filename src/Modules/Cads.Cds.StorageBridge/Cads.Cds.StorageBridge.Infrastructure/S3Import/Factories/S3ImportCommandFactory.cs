using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

public class S3ImportCommandFactory : IS3ImportCommandFactory
{
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction? _transaction;
    private NpgsqlCommand? _getSchemaColumnsCommand;
    private static readonly NpgsqlCommandBuilder s_commandBuilder = new();

    public S3ImportCommandFactory(NpgsqlConnection connection) : this(connection, null)
    {
    }

    public S3ImportCommandFactory(NpgsqlConnection connection, NpgsqlTransaction? transaction)
    {
        ArgumentNullException.ThrowIfNull(connection, nameof(connection));

        _connection = connection;
        _transaction = transaction;
    }

    protected virtual string GenerateTempTableSql(ImportDataType importDataType, SchemaName schemaName, long fileImportId)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var commandText = $"CREATE TEMP TABLE {tempTableName} (LIKE {tableName} INCLUDING DEFAULTS EXCLUDING CONSTRAINTS) ON COMMIT DROP;";

        if (schemaName == SchemaName.CtsTransactions)
        {
            commandText += $"ALTER TABLE {tempTableName} " +
                "DROP COLUMN trans_id, " +
                "ALTER COLUMN trans_type SET DEFAULT 'B', " +
                $"ALTER COLUMN cts_file_import_id SET DEFAULT {fileImportId};";
        }

        return commandText;
    }

    protected virtual async Task<string> GenerateInsertSqlAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(importDataType, schemaName);
        var tempTableName = GetTableName(importDataType, schemaName, isTemp: true);
        var columnNames = await GetColumnNamesAsync(importDataType, schemaName, cancellationToken);
        var insertColumns = string.Join(",", columnNames);

        return $"INSERT INTO {tableName} ({insertColumns}) " +
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

    public DbCommand CreateTempTableCommand(ImportDataType importDataType, SchemaName schemaName, long fileImportId)
    {
        var sql = GenerateTempTableSql(importDataType, schemaName, fileImportId);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection,
            Transaction = _transaction
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
            Connection = _connection,
            Transaction = _transaction
        };
    }

    public async Task<DbCommand> CreateUpdateCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpdateSqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection,
            Transaction = _transaction
        };
    }

    public async Task<DbCommand> CreateUpsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpsertSqlAsync(importDataType, schemaName, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection,
            Transaction = _transaction
        };
    }

    public static string GetTableName(ImportDataType importDataType, SchemaName schemaName, bool isTemp = false)
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

        var primaryKey = importDataType.GetTableInfoAttribute(schemaName)?.PrimaryKey
            ?? throw new ArgumentException("Primarykey cannot be null", nameof(importDataType));

        _getSchemaColumnsCommand ??= CreateGetSchemaColumnsCommand(_connection);

        _getSchemaColumnsCommand.Parameters["tableName"].Value = tableName;
        _getSchemaColumnsCommand.Parameters["schema"].Value = (object?)schema ?? DBNull.Value;
        _getSchemaColumnsCommand.Parameters["primaryKey"].Value = primaryKey;

        await using var reader = await _getSchemaColumnsCommand.ExecuteReaderAsync(cancellationToken);

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

    private static NpgsqlCommand CreateGetSchemaColumnsCommand(NpgsqlConnection connection)
    {
        var query = @"
            SELECT column_name 
            FROM information_schema.columns
            WHERE table_name = @tableName
            AND column_name != @primaryKey
            AND (@schema IS NULL OR table_schema = @schema)
            AND is_generated = 'NEVER'
            ORDER BY ordinal_position";

        var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("tableName", NpgsqlDbType.Varchar, DBNull.Value);
        command.Parameters.AddWithValue("schema", NpgsqlDbType.Varchar, DBNull.Value);
        command.Parameters.AddWithValue("primaryKey", NpgsqlDbType.Varchar, DBNull.Value);
        command.Prepare();

        return command;
    }
}