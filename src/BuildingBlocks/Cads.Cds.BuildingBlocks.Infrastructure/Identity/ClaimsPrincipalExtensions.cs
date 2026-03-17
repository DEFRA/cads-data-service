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
        ?? user.FindFirst("preferred_username")?.Value;

    public static string? GetDisplayName(this ClaimsPrincipal user) =>
        user.FindFirst("name")?.Value
        ?? user.GetEmail()
        ?? user.GetOid();

    public static string? GetTenantId(this ClaimsPrincipal user) =>
        user.FindFirst(CustomClaimTypes.TenantId)?.Value
        ?? user.FindFirst(CustomClaimTypes.CustomTenantId)?.Value;
}