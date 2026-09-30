using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

public abstract record DbAdminRequestBaseDto(string Command, JsonElement? Args);