using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;
using System;
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

        var json = (string?)await cmd.ExecuteScalarAsync(cancellationToken);
        return JsonDocument.Parse(json ?? "{}");
    }
}