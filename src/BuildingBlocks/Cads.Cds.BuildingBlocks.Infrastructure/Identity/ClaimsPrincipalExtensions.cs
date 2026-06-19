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
        // 1. Real user email claims
        var email = GetEmailLikeClaim(user);
        if (!string.IsNullOrWhiteSpace(email))
            return email;

        // 2. Service account identifiers
        return GetServiceAccountIdentifier(user);
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
        // 1. Real user email claims
        var email = GetEmailLikeClaim(user);
        if (!string.IsNullOrWhiteSpace(email))
            return email;

        // 2. Service account identifiers
        var svc = GetServiceAccountIdentifier(user);
        if (!string.IsNullOrWhiteSpace(svc))
            return svc;

        // 3. Final fallback
        return user.GetOid();
    }

    private static string? GetEmailLikeClaim(ClaimsPrincipal user) =>
        user.FindFirst(ClaimTypes.Email)?.Value ??
        user.FindFirst("email")?.Value ??
        user.FindFirst("preferred_username")?.Value;

    private static string? GetServiceAccountIdentifier(ClaimsPrincipal user) =>
        user.FindFirst("unique_name")?.Value ??
        user.FindFirst("upn")?.Value;
}