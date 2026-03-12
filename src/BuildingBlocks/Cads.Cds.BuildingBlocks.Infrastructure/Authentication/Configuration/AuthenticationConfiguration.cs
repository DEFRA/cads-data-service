namespace Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;

public class AuthenticationConfiguration
{
    public AuthenticationStateConfiguration ApiKey { get; set; } = new();
    public AuthenticationProviderConfiguration Cognito { get; set; } = new();
    public AuthenticationProviderConfiguration AzureAD { get; set; } = new();
}

public class AuthenticationStateConfiguration
{
    public bool Enabled { get; set; }
}

public class AuthenticationProviderConfiguration : AuthenticationStateConfiguration
{
    public string? Authority { get; set; }
    public string? Audience { get; set; }
}