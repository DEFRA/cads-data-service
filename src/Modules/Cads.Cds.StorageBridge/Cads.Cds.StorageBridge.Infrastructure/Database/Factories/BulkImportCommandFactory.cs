using Cads.Cds.StorageBridge.Core.Domain;
using Cads.Cds.StorageBridge.Core.Extensions;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.Database.Factories;

public class BulkImportCommandFactory(NpgsqlConnection connection) : IBulkImportCommandFactory
{
    public DbCommand CreateTempTableCommand(BulkImportType bulkImportType) => new NpgsqlCommand
    {
        CommandText = $"CREATE TEMP TABLE {GetTempTableName(bulkImportType)} " +
            $"record_type TEXT, " +
            $"record_count BIGINT, " +
            $"LIKE {bulkImportType.GetTableName()} INCLUDING ALL " +
            $"ON COMMIT DROP",
        Connection = connection
    };

    public StreamWriter CreateTextImport(BulkImportType bulkImportType, char delimiter) =>
        connection.BeginTextImport(
            $"COPY {GetTempTableName(bulkImportType)} " +
            $"FROM STDIN WITH (FORMAT csv, DELIMITER '{delimiter}', HEADER true)");

    public DbCommand CreateUpsertCommand(BulkImportType bulkImportType) => new NpgsqlCommand
    {
        CommandText = $"INSERT INTO {bulkImportType.GetTableName()} " +
            $"SELECT * FROM {GetTempTableName(bulkImportType)} WHERE [record_type]='I' OR [record_type]='U' " +
            $"ON CONFLICT (id) DO UPDATE " +
            $"SET {CreateUpsertSetClause(bulkImportType)}",
        Connection = connection
    };

    public DbCommand CreateDeleteCommand(BulkImportType bulkImportType) => new NpgsqlCommand
    {
        CommandText = $"DELETE FROM {bulkImportType.GetTableName()} " + 
            $"WHERE id IN (SELECT id FROM {GetTempTableName(bulkImportType)} WHERE [record_type]='D')",
        Connection = connection
    };

    public DbCommand CreateSetContraintStateCommand(BulkImportType bulkImportType, bool state) => new NpgsqlCommand
    {
        CommandText = $"ALTER TABLE {bulkImportType.GetTableName()} " +
            $"{(state ? "ENABLE" : "DISABLE")} TRIGGER ALL",
        Connection = connection
    };

    public DbCommand CreateSetDeferredAllContraintCommand() => new NpgsqlCommand
    {
        CommandText = $"SET CONSTRAINTS ALL DEFERRED",
        Connection = connection
    };

    private static string GetTempTableName(BulkImportType bulkImportType) => $"temp_{bulkImportType.GetTableName()}";

    private string CreateUpsertSetClause(BulkImportType bulkImportType)
    {
        var columnNames = GetColumnNamesAsync(bulkImportType).GetAwaiter().GetResult();
        return CreateUpsertSetClause(columnNames);
    }

    private static string CreateUpsertSetClause(List<string> columnNames)
    {
        // Exclude 'id' and 'record_type' from the update set clause
        var columnsToUpdate = columnNames.Where(c => c != "id" && c != "record_type");
        return string.Join(", ", columnsToUpdate.Select(c => $"{c} = EXCLUDED.{c}"));
    }

    private async Task<List<string>> GetColumnNamesAsync(BulkImportType bulkImportType, CancellationToken cancellationToken = default)
    {
        var tableName = bulkImportType.GetTableName();
        var query = $"SELECT column_name FROM information_schema.columns " +
            $"WHERE table_name = '{tableName}' " +
            $"ORDER BY ordinal_position";

        using var command = new NpgsqlCommand(query, connection);
        var columnNames = new List<string>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            columnNames.Add(reader.GetString(0));
        }

        return columnNames;
    }
}