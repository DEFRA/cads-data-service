using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;

public class AuthenticationOptionsConfigurator(IOptions<AuthenticationConfiguration> authConfig) : IConfigureOptions<AuthenticationOptions>
{
    private readonly AuthenticationConfiguration _authConfig = authConfig.Value;

    public void Configure(AuthenticationOptions options)
    {
        // Neutral fallback; policies decide the real scheme
        options.DefaultScheme = AuthenticationConstants.ApiKeySchemeName;
        options.DefaultAuthenticateScheme = AuthenticationConstants.ApiKeySchemeName;
        options.DefaultChallengeScheme = AuthenticationConstants.ApiKeySchemeName;
    }
}