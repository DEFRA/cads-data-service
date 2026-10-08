using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;

public record DbAdminCommandResponse(
    string Command,
    JsonElement Result
);