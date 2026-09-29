using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

public record DbAdminExecuteCommandRequestDto(string Command, JsonElement? Args)
    : DbAdminRequestBaseDto(Command, Args);