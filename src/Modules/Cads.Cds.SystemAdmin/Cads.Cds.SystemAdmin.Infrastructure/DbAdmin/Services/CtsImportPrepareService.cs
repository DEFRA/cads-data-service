using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;

public class CtsImportPrepareService(IPostgresDataSourceFactory factory) : ICtsImportPrepareService
{
    private const bool DeleteSource = true;
    private const long RangeSize = 100_000;
    private const bool TruncateCts = false;

    // p_run_id is an INOUT parameter. Passing NULL::bigint at the top level of a CALL is allowed
    // and the procedure returns the created run id as a single-column row.
    private const string PrepareSql = """
        CALL cads.prepare_cts_parallel_bulk_import(
            p_delete_source => @delete_source,
            p_range_size    => @range_size,
            p_run_id        => NULL::bigint,
            p_truncate_cts  => @truncate_cts)
        """;

    public async Task<long> PrepareAsync(CancellationToken cancellationToken = default)
    {
        var dataSource = factory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);
        await using var conn = await dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = conn.CreateCommand();

        cmd.CommandText = PrepareSql;
        cmd.CommandTimeout = 0; // planning runs as one transaction and can take 20-30 minutes
        cmd.Parameters.AddWithValue("delete_source", DeleteSource);
        cmd.Parameters.AddWithValue("range_size", RangeSize);
        cmd.Parameters.AddWithValue("truncate_cts", TruncateCts);

        var result = await cmd.ExecuteScalarAsync(cancellationToken);

        return result is long runId
            ? runId
            : throw new InvalidOperationException("prepare_cts_parallel_bulk_import did not return a run id.");
    }
}