using Cads.Cds.BuildingBlocks.Application.Identity;
using System.Security.Claims;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Identity;

public static class ClaimsPrincipalExtensions
{
    public static string? GetOid(this ClaimsPrincipal user) =>
        user.FindFirst(CustomClaimTypes.Oid)?.Value
        ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? user.FindFirst(CustomClaimTypes.CognitoSub)?.Value;

    public static string? GetEmail(this ClaimsPrincipal user) =>
        user.FindFirst(ClaimTypes.Email)?.Value
        ?? user.FindFirst("email")?.Value
        ?? user.FindFirst("preferred_username")?.Value;

    public static string? GetDisplayName(this ClaimsPrincipal user) =>
        user.FindFirst("name")?.Value
        ?? user.GetEmail()
        ?? user.GetOid();

    public static string? GetTenantId(this ClaimsPrincipal user) =>
        user.FindFirst(CustomClaimTypes.TenantId)?.Value
        ?? user.FindFirst(CustomClaimTypes.CustomTenantId)?.Value;

    public static string? GetUserIdentifier(this ClaimsPrincipal user)
    {
        // 1. Email (standard users)
        var email = user.FindFirst(ClaimTypes.Email)?.Value
            ?? user.FindFirst("email")?.Value;
        if (!string.IsNullOrWhiteSpace(email))
            return email;

        // 2. unique_name (standard users + service accounts)
        var uniqueName = user.FindFirst("unique_name")?.Value;
        if (!string.IsNullOrWhiteSpace(uniqueName))
            return uniqueName;

        // 3. upn (fallback for service accounts)
        var upn = user.FindFirst("upn")?.Value;
        if (!string.IsNullOrWhiteSpace(upn))
            return upn;

        // 4. Final fallback to OID or sub
        return user.GetOid();
    }
}