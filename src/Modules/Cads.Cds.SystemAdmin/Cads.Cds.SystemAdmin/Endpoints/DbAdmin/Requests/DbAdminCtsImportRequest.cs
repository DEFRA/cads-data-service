using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;

public record DbAdminCtsImportRequest(string Command, JsonElement? Args)
    : DbAdminRequestBase(Command, Args);