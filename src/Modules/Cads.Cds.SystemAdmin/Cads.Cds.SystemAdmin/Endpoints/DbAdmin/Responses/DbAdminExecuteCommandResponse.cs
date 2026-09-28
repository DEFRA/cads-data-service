using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;

public record DbAdminExecuteCommandResponse(
    string Command,
    JsonElement Result
);