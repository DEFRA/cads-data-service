using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;

public abstract record DbAdminRequestBase(string Command, JsonElement? Args);