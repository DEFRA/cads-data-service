using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;

public record DbAdminExecuteCommandRequest(
    string Command,
    JsonElement? Args
);