using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;

namespace Cads.Cds.SystemAdmin.Testing.Support.Utilities.Postgres;

public static class PromotionPostgresDbCommandHelper
{
    public const string ImportRunStatusPlanning = "planning";
    public const string ImportRunStatusComplete = "complete";
    public const string ImportRunStatusFailed = "failed";

    public static async Task<long> InsertCtsParallelImportRunAsync(
        this PostgresDb postgresDb,
        string correlationId,
        string status)
    {
        var insertQuery = @"INSERT INTO cads.cts_parallel_import_runs(
            status
            , delete_source
            , range_size
            , truncate_cts
            , correlation_id)
                VALUES
                    (@status, false, 100000, false, @correlationId)
            ON CONFLICT DO NOTHING
            RETURNING run_id;";

        var testRunId = await postgresDb.ExecuteScalarAsync<long>(
          insertQuery,
          cmd =>
          {
              cmd.Parameters.AddWithValue("status", status);
              cmd.Parameters.AddWithValue("correlationId", correlationId);
          });

        return testRunId;
    }

    public static async Task<System.Data.DataSet> GetCtsParallelImportRunsByCorrelationIdAsync(this PostgresDb postgresDb, string correlationId)
    {
        var dataSet = await postgresDb.FillDataSetAsync(
            "SELECT * FROM cads.cts_parallel_import_runs WHERE correlation_id = @correlationId",
            cmd =>
            {
                cmd.Parameters.AddWithValue("correlationId", correlationId);
            });

        return dataSet;
    }

    public static async Task UpdateCtsParallelImportRunStatusByCorrelationIdAsync(this PostgresDb postgresDb, string correlationId, string status)
    {
        await postgresDb.ExecuteNonQueryAsync(
            "UPDATE cads.cts_parallel_import_runs SET status = @status WHERE correlation_id = @correlationId;",
            cmd =>
            {
                cmd.Parameters.AddWithValue("correlationId", correlationId);
                cmd.Parameters.AddWithValue("status", status);
            });
    }

    public static async Task UpdateAllCtsParallelImportRunStatusAsync(this PostgresDb postgresDb, string status)
    {
        await postgresDb.ExecuteNonQueryAsync(
            "UPDATE cads.cts_parallel_import_runs SET status = @status;",
            cmd =>
            {
                cmd.Parameters.AddWithValue("status", status);
            });
    }
}