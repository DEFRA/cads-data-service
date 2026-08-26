using Cads.Cds.ApiSurface.Dtos.Imports;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;

public static class FileImportPostgresDbCommandHelper
{
    public static async Task<long> InsertFileImportAsync(this PostgresDb postgresDb,  string importFileName, string groupKey, FileImportStatus fileImportStatus, string? lastfilePartImported = null, long rowsImported = 0)
    {
        var insertQuery = @"INSERT INTO cads.cts_file_imports(
            destination_table_name
            , file_name
            , total_rows_to_process
            , added_at
            , import_status_id
            , processing_status_id
            , rows_found
            , import_start_at
            , import_end_at
            , batch_date
            , group_key
            , import_type
            , failed_attempts
            , last_error_reason
            , last_file_part_imported
            , rows_imported)
                VALUES
                    ('dtn', @fileName, 1, NOW(), @fileImportStatus, 1, 1, NULL, NULL, NOW(), @groupKey, 'BULK', 0, NULL, @lastfilePartImported, @rowsImported)
            ON CONFLICT DO NOTHING
            RETURNING cts_file_import_id;";

        var testFileImportId = await postgresDb.ExecuteScalarAsync<long>(
          insertQuery,
          cmd =>
          {
              cmd.Parameters.AddWithValue("fileName", importFileName);
              cmd.Parameters.AddWithValue("fileImportStatus", (int)fileImportStatus);
              cmd.Parameters.AddWithValue("groupKey", groupKey);
              cmd.Parameters.AddWithValue("lastfilePartImported", ((object?)lastfilePartImported) ?? DBNull.Value);
              cmd.Parameters.AddWithValue("rowsImported", rowsImported);
          });

        return testFileImportId;
    }

    public static async Task DeleteFileImportByFileNameAsync(this PostgresDb postgresDb, string fileName)
    {
        await postgresDb.ExecuteNonQueryAsync(
            "DELETE FROM cads.cts_file_imports WHERE file_name = @fileName;",
            cmd =>
            {
                cmd.Parameters.AddWithValue("fileName", fileName);
            });
    }

    public static async Task DeleteFileImportByGroupKeyAsync(this PostgresDb postgresDb, string groupKey)
    {
        await postgresDb.ExecuteNonQueryAsync(
            "DELETE FROM cads.cts_file_imports WHERE group_key = @groupKey;",
            cmd =>
            {
                cmd.Parameters.AddWithValue("groupKey", groupKey);
            });
    }

    public static async Task<System.Data.DataSet> GetFileImportDataSetByFileName(this PostgresDb postgresDb, string fileName)
    {
        var dataSet = await postgresDb.FillDataSetAsync(
            "SELECT * FROM cads.cts_file_imports WHERE file_name = @fileName", 
            cmd =>
            {
                cmd.Parameters.AddWithValue("fileName", fileName);
            });

        return dataSet;
    }

    public static async Task<System.Data.DataSet> GetFileImportDataSetByGroupKey(this PostgresDb postgresDb, string groupKey)
    {
        var dataSet = await postgresDb.FillDataSetAsync(
            "SELECT * FROM cads.cts_file_imports WHERE group_key = @groupKey", 
            cmd =>
            {
                cmd.Parameters.AddWithValue("groupKey", groupKey);
            });

        return dataSet;
    }
}
