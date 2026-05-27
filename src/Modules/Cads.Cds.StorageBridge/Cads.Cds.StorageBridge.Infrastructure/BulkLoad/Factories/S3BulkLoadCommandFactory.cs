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

    protected virtual string GenerateTempTableSql(BulkLoadDataTypes bulkImportType)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        return $"CREATE TEMP TABLE {tempTableName} (LIKE {tableName} INCLUDING ALL) ON COMMIT DROP;";
    }

    protected virtual async Task<string> GenerateInsertSqlAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
               $"SELECT {string.Join(",", columnNames)} FROM {tempTableName}";
    }

    protected virtual async Task<string> GenerateUpdateSqlAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return $"UPDATE {tableName} AS m SET {string.Join(", ", columnNames.Select(col => $"m.{col} = t.{col}"))} " +
               $"FROM {tempTableName} AS t WHERE m.{key} = t.{key}";
    }

    protected virtual async Task<string> GenerateUpsertSqlAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
               $"SELECT {string.Join(",", columnNames)} FROM {tempTableName} " +
               $"ON CONFLICT ({key}) DO UPDATE SET {string.Join(", ", columnNames.Select(c => $"{c} = EXCLUDED.{c}"))}";
    }

    protected virtual async Task<string> GenerateDeleteSqlAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return $"DELETE FROM {tableName} WHERE {key} IN (SELECT {key} FROM {tempTableName})";
    }

    protected virtual async Task<string> GenerateTempTableQuerySqlAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken)
    {
        var tempTableName = GetTableName(bulkImportType, isTemp: true);
        var columnNames = string.Join(',', await GetColumnNamesAsync(bulkImportType, cancellationToken));
        return $"SELECT {columnNames} FROM {tempTableName}";
    }

    public DbCommand CreateTempTableCommand(BulkLoadDataTypes bulkImportType)
    {
        var sql = GenerateTempTableSql(bulkImportType);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public StreamWriter CreateTextImport(BulkLoadDataTypes bulkImportType, char delimiter, IEnumerable<string> columns)
    {
        return _connection.BeginTextImport(
            $"COPY {GetTableName(bulkImportType, isTemp: true)} ({string.Join(",", columns)}) " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER false)");
    }

    public async Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateInsertSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpdateSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateUpsertSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateDeleteSqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public async Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default)
    {
        var sql = await GenerateTempTableQuerySqlAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = sql,
            Connection = _connection
        };
    }

    public string GetTableName(BulkLoadDataTypes bulkImportType, bool isTemp = false)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        return s_commandBuilder.QuoteIdentifier(isTemp ? $"temp_{tableName}" : tableName);
    }

    public async Task<List<string>> FilterColumnsToTableAsync(
        BulkLoadDataTypes bulkImportType,
        IEnumerable<string> fileColumns,
        CancellationToken cancellationToken = default)
    {
        var dbColumns = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return [.. fileColumns.Where(c => dbColumns.Contains(c, StringComparer.OrdinalIgnoreCase))];
    }

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB. Made virtual so can use test friendly factory.
    /// </summary>
    /// <param name="bulkImportType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [ExcludeFromCodeCoverage]
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

        await using var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("tableName", tableName);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
            columnNames.Add(reader.GetString(0));

        return columnNames;
    }
}