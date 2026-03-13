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
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.Name, "jwt-user"),
        new Claim("scope", "read")
        ], AuthenticationConstants.CognitoSchemeName);

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationConstants.CognitoSchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}