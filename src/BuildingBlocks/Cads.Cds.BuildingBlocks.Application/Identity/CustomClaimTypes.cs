namespace Cads.Cds.BuildingBlocks.Application.Identity;

public static class CustomClaimTypes
{
    public const string Oid = "oid";
    public const string TenantId = "tid";
    public const string CognitoSub = "sub";
    public const string CustomTenantId = "custom:tenantId";

    // Raw (unmapped) role claim names: Azure AD issues "roles"; the local OIDC mock issues "role"
    public const string Roles = "roles";
    public const string Role = "role";
}