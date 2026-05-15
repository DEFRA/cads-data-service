using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.Extensions;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public class S3BulkLoadCommandFactory(NpgsqlConnection connection) : IS3BulkLoadCommandFactory
{
    // Use a single NpgsqlCommandBuilder instance to call instance methods like QuoteIdentifier
    private static readonly NpgsqlCommandBuilder s_commandBuilder = new();

    private const string RecordTypeColumnName = "record_type";
    private const string RowNumberColumnName = "row_number";

    private static class RecordType
    {
        public const string Delete = "D";
        public const string Insert = "I";
        public const string Update = "U";
    }

    public DbCommand CreateTempTableCommand(BulkLoadDataType bulkImportType)
    {
        // Get quoted table identifiers for the target and its temporary staging table
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, true);

        // Quote column names to ensure identifiers are safe for SQL generation
        var quotedRowNumberColumnName = s_commandBuilder.QuoteIdentifier(RowNumberColumnName);

        return new NpgsqlCommand
        {
            CommandText = $"CREATE TEMP TABLE {tempTableName} (" +
                $"LIKE {tableName} INCLUDING ALL) " +
                $"ON COMMIT DROP; " +
                $"ALTER TABLE {tempTableName} DROP COLUMN {quotedRowNumberColumnName};",
            Connection = connection
        };
    }

    public StreamWriter CreateTextImport(BulkLoadDataType bulkImportType, char delimiter) =>
      connection.BeginTextImport(
          $"COPY {GetTableName(bulkImportType, true)} " +
          $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER true)");

    public StreamWriter CreateTextImport(BulkLoadDataType bulkImportType, char delimiter, IEnumerable<string> columns) =>
        connection.BeginTextImport(
            $"COPY {GetTableName(bulkImportType, true)} ({string.Join(",", columns)}) " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER true)");

    public DbCommand CreateReindexCommand(BulkLoadDataType bulkImportType)
    {
        var tableName = GetTableName(bulkImportType);

        return new NpgsqlCommand
        {
            CommandText = $"REINDEX TABLE {tableName}",
            Connection = connection
        };
    }

    public async Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return new NpgsqlCommand
        {
            CommandText = $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
                $"SELECT {string.Join(",", columnNames)} FROM {tempTableName} " +
                // Exclude record type check from the upsert operation
                //$"WHERE {RecordTypeColumnName} IN ({RecordType.Insert}','{RecordType.Update}')" +
                $"ON CONFLICT ({key}) DO UPDATE " +
                $"SET {CreateUpsertSetClause(columnNames)}",
            Connection = connection
        };
    }

    public async Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);

        return new NpgsqlCommand
        {
            CommandText = $"INSERT INTO {tableName} ({string.Join(",", columnNames)}) " +
                $"SELECT {string.Join(",", columnNames)} FROM {tempTableName} ",
            // Exclude record type check from the upsert operation
            //$"WHERE {RecordTypeColumnName} IN ('{RecordType.Insert}')" +
            Connection = connection
        };
    }

    public async Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return new NpgsqlCommand
        {
            CommandText = $"UPDATE {tableName} AS m " +
                $"SET {string.Join(", ", columnNames.Select(col => $"m.{col} = t.{col}"))} " +
                $"FROM {tempTableName} AS t " +
                $"WHERE m.{key} = t.{key} ",
            // Exclude record type check from the upsert operation
            //$"WHERE {RecordTypeColumnName} IN ('{RecordType.Update}')" +
            Connection = connection
        };
    }

    public async Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = GetTableName(bulkImportType);
        var tempTableName = GetTableName(bulkImportType, true);
        var columnNames = await GetColumnNamesAsync(bulkImportType, cancellationToken);
        var key = bulkImportType.GetTableKey() ?? columnNames[0];

        return new NpgsqlCommand
        {
            CommandText = $"DELETE FROM {tableName} " +
                $"WHERE {key} IN " +
                $"(SELECT {key} FROM {tempTableName} WHERE {RecordTypeColumnName}='{RecordType.Delete}')",
            Connection = connection
        };
    }

    public DbCommand CreateSetContraintStateCommand(BulkLoadDataType bulkImportType, bool state) => new NpgsqlCommand
    {
        CommandText = $"ALTER TABLE {GetTableName(bulkImportType)} " +
            $"{(state ? "ENABLE" : "DISABLE")} TRIGGER ALL",
        Connection = connection
    };

    public DbCommand CreateSetDeferredAllContraintCommand() => new NpgsqlCommand
    {
        CommandText = $"SET CONSTRAINTS ALL DEFERRED",
        Connection = connection
    };

    public async Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tempTableName = GetTableName(bulkImportType, true);
        var columnNames = string.Join(',', await GetColumnNamesAsync(bulkImportType, cancellationToken));

        return new NpgsqlCommand
        {
            CommandText = $"SELECT {columnNames} FROM {tempTableName}",
            Connection = connection
        };
    }

    private static string GetTableName(BulkLoadDataType bulkImportType, bool isTemp = false)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        return s_commandBuilder.QuoteIdentifier(isTemp ? $"temp_{tableName}" : tableName);
    }

    private static string CreateUpsertSetClause(List<string> columnNames)
    {
        // Exclude 'record_type' from the update set clause
        var columnsToUpdate = columnNames.Where(c => c != RecordTypeColumnName);
        return string.Join(", ", columnsToUpdate.Select(c => $"{c} = EXCLUDED.{c}"));
    }

    private async Task<List<string>> GetColumnNamesAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = bulkImportType.GetTableName()
            ?? throw new ArgumentException("Table name cannot be null", nameof(bulkImportType));

        var columnNames = new List<string>();

        const string query = @"
            SELECT column_name 
            FROM information_schema.columns
            WHERE table_name = @tableName
            AND column_name <> @excludeColumnName
            ORDER BY ordinal_position";

        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("tableName", tableName);
        command.Parameters.AddWithValue("excludeColumnName", RowNumberColumnName);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            columnNames.Add(reader.GetString(0));
        }

        return columnNames;
    }
}