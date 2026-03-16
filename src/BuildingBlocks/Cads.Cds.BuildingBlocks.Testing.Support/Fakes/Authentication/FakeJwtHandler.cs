using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

public class FakeJwtHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "jwt-user")
        };

        if (Scheme.Name == AuthenticationConstants.AzureADSchemeName)
        {
            claims.Add(new Claim("scope", AuthenticationConstants.AzureADClaims.ReportsReadScopeName));
        }
        else if (Scheme.Name == AuthenticationConstants.CognitoSchemeName)
        {
            claims.Add(new Claim("scope", AuthenticationConstants.CognitoClaims.AccessScopeName));
        }

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}