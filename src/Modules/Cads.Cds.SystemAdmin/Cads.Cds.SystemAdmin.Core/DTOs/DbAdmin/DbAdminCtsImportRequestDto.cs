using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

public record DbAdminCtsImportRequestDto(string Command, JsonElement? Args)
    : DbAdminRequestBaseDto(Command, Args);