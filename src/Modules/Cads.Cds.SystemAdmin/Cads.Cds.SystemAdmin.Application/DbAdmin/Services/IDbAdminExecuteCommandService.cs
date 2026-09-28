using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Application.DbAdmin.Services;

public interface IDbAdminExecuteCommandService
{
    Task<JsonDocument> ExecuteAsync(string command, JsonElement? args, CancellationToken cancellationToken = default);
}