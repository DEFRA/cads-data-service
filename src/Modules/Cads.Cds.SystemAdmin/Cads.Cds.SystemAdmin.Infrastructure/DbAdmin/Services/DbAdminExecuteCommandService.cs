using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;

public class DbAdminExecuteCommandService(IPostgresDataSourceFactory factory) : IDbAdminExecuteCommandService
{
    public async Task<JsonDocument> ExecuteAsync(string command, JsonElement? args, CancellationToken cancellationToken = default)
    {
        var dataSource = factory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);

        await using var conn = await dataSource.OpenConnectionAsync(cancellationToken);
        await using var cmd = conn.CreateCommand();

        cmd.CommandText = "SELECT cads.db_admin_exec_command(@command, @args)";
        cmd.Parameters.AddWithValue("command", command);
        cmd.Parameters.AddWithValue("args", args ?? JsonDocument.Parse("{}").RootElement);

        var json = (string?)await cmd.ExecuteScalarAsync(cancellationToken);
        return JsonDocument.Parse(json ?? "{}");
    }
}