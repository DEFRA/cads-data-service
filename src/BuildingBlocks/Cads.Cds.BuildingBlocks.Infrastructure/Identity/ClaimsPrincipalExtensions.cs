using Cads.Cds.BuildingBlocks.Application.Identity;
using System.Security.Claims;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Identity;

public static class ClaimsPrincipalExtensions
{
    public static string? GetOid(this ClaimsPrincipal user) =>
        user.FindFirst(CustomClaimTypes.Oid)?.Value
        ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? user.FindFirst(CustomClaimTypes.CognitoSub)?.Value;

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        // Email (standard users)
        var email =
            user.FindFirst(ClaimTypes.Email)?.Value ??
            user.FindFirst("email")?.Value ??
            user.FindFirst("preferred_username")?.Value;

        if (!string.IsNullOrWhiteSpace(email))
            return email;

        // Service accounts + fallback identifiers
        var uniqueName = user.FindFirst("unique_name")?.Value;
        if (!string.IsNullOrWhiteSpace(uniqueName))
            return uniqueName;

        var upn = user.FindFirst("upn")?.Value;
        if (!string.IsNullOrWhiteSpace(upn))
            return upn;

        return null;
    }

    public static string? GetDisplayName(this ClaimsPrincipal user) =>
        user.FindFirst("name")?.Value
        ?? user.GetEmail()
        ?? user.FindFirst("unique_name")?.Value
        ?? user.GetOid();

    public static string? GetTenantId(this ClaimsPrincipal user) =>
        user.FindFirst(CustomClaimTypes.TenantId)?.Value
        ?? user.FindFirst(CustomClaimTypes.CustomTenantId)?.Value;

    public static string? GetUserIdentifier(this ClaimsPrincipal user)
    {
        // Email (standard users)
        var email =
            user.FindFirst(ClaimTypes.Email)?.Value ??
            user.FindFirst("email")?.Value ??
            user.FindFirst("preferred_username")?.Value;

        if (!string.IsNullOrWhiteSpace(email))
            return email;

        // unique_name (standard users + service accounts)
        var uniqueName = user.FindFirst("unique_name")?.Value;
        if (!string.IsNullOrWhiteSpace(uniqueName))
            return uniqueName;

        // upn (fallback for service accounts)
        var upn = user.FindFirst("upn")?.Value;
        if (!string.IsNullOrWhiteSpace(upn))
            return upn;

        // Final fallback to OID or sub
        return user.GetOid();
    }
}