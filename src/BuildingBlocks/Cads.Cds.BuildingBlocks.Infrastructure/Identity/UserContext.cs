using Cads.Cds.BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Http;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Identity;

public class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;

        Oid = user?.GetOid();
        Email = user?.GetEmail();
        DisplayName = user?.GetDisplayName();
        TenantId = user?.GetTenantId();
    }

    public string? Oid { get; }
    public string? Email { get; }
    public string? DisplayName { get; }
    public string? TenantId { get; }
}