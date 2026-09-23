namespace Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;

public class AuthenticationConfiguration
{
    public AuthenticationStateConfiguration ApiKey { get; set; } = new();
    public AwsOutboundFederationConfiguration AwsOutboundFederation { get; set; } = new();
    public AuthenticationProviderConfiguration Cognito { get; set; } = new();
    public AuthenticationProviderConfiguration AzureAD { get; set; } = new();
}

public class AuthenticationStateConfiguration
{
    public bool Enabled { get; set; }
}

public class AwsOutboundFederationConfiguration : AuthenticationStateConfiguration
{
    public string Audience { get; set; } = string.Empty;      // this service's own identity
    public string TrustedOrgId { get; set; } = string.Empty;  // reject tokens outside DEFRA's AWS Org
    public string Environment { get; set; } = string.Empty;   // reject cross-environment replay
}

public class AuthenticationProviderConfiguration : AuthenticationStateConfiguration
{
    public string? Authority { get; set; }
    public string? Audience { get; set; }
    public string? MetadataAddress { get; set; }
    public bool RequireHttpsMetadata { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public string RoleClaimType { get; set; } = "http://schemas.microsoft.com/identity/claims/scope";
}

public static class AuthenticationConstants
{
    public const string ApiKeySchemeName = "Basic";
    public const string AwsStsSchemeName = "AwsSts";
    public const string CognitoSchemeName = "Cognito";
    public const string AzureADSchemeName = "AzureAd";

    public const string ScopeClaimType = "scope";

    public const string StsOrCognitoPolicy = "StsOrCognito";
    public const string StsOrCognitoPolicyImports = "StsOrCognitoImports";

    public const string ApiKeyOrCognitoPolicy = "ApiKeyOrCognito";
    public const string AadReportsReadPolicy = "AadReportsRead";
}