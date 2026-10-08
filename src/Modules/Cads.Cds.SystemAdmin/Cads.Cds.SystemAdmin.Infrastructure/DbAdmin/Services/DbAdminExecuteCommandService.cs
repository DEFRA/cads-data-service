using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;

public class DbAdminExecuteCommandService(IPostgresDataSourceFactory factory) : IDbAdminExecuteCommandService
{
    public async Task<JsonDocument> ExecuteAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : DbAdminRequestBaseDto
    {
        var storedProcedure = request switch
        {
            DbAdminExecuteCommandRequestDto => "SELECT cads.db_admin_exec_command(@command, @args)",
            DbAdminCtsImportRequestDto => "SELECT cads.cts_parallel_import_admin_exec_command(@command, @args)",
            _ => throw new NotSupportedException($"Unsupported request type '{typeof(TRequest).Name}'.")
        };

        var dataSource = factory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);
        await using var conn = await dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = conn.CreateCommand();

        cmd.CommandText = storedProcedure;
        cmd.Parameters.AddWithValue("command", request.Command);
        cmd.Parameters.AddWithValue("args", request.Args ?? JsonDocument.Parse("{}").RootElement);

        var json = await cmd.ExecuteScalarAsync(cancellationToken) as string;
        return JsonDocument.Parse(json ?? "{}");
    }

    public async Task<IReadOnlyList<CtsImportRunDto>> GetCtsImportRunsAsync(CancellationToken cancellationToken = default)
    {
        var dataSource = factory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);
        await using var conn = await dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = conn.CreateCommand();

        cmd.CommandText = """
            SELECT run_id, status, created_at, bulk_completed_at, completed_at
            FROM cads.cts_parallel_import_runs
            ORDER BY run_id DESC
            """;

        var runs = new List<CtsImportRunDto>();
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            runs.Add(new CtsImportRunDto(
                reader.GetInt64(0),
                reader.GetString(1),
                reader.GetFieldValue<DateTimeOffset>(2),
                await reader.IsDBNullAsync(3, cancellationToken) ? null : reader.GetFieldValue<DateTimeOffset>(3),
                await reader.IsDBNullAsync(4, cancellationToken) ? null : reader.GetFieldValue<DateTimeOffset>(4)));
        }

        return runs;
    }
}